using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Domain.Enums;
using ExpressDeliveryMail.Service.Extensions;
using ExpressDeliveryMail.Service.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace ExpressDeliveryMail.UI.OtherMenus;

public class UserMenu
{
    private readonly UserService _userService;

    public UserMenu(UserService userService)
    {
        _userService = userService;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]User Menu[/]")
                    .PageSize(6)
                    .AddChoices(new[] { "Create User", "View All Users", "Update User", "Deposit", "Delete User", "[red]Go Back[/]" }));

            switch (choice)
            {
                case "Create User":
                    AnsiConsole.Clear();
                    await CreateUserAsync();
                    break;
                case "View All Users":
                    AnsiConsole.Clear();
                    await ViewAllUsersAsync();
                    break;
                case "Update User":
                    AnsiConsole.Clear();
                    await UpdateUserAsync();
                    break;
                case "Deposit":
                    AnsiConsole.Clear();
                    await DepositAsync();
                    break;
                case "Delete User":
                    AnsiConsole.Clear();
                    await DeleteUserAsync();
                    break;
                case "[red]Go Back[/]":
                    return;
            }
        }
    }

    private async Task CreateUserAsync()
    {
        var user = new UserCreationModel();

        user.FirstName = AnsiConsole.Ask<string>("Enter First Name:");
        user.LastName = AnsiConsole.Ask<string>("Enter Last Name:");
        user.Phone = GetPhoneInput("Enter phone number: ");
        user.Email = AnsiConsole.Ask<string>("Enter Email:");
        user.Role = AnsiConsole.Prompt(
            new SelectionPrompt<UserRole>()
                .Title("Select new Role:")
                .PageSize(3)
                .AddChoices(new[]
                {
                    UserRole.Admin,
                    UserRole.Sender
                }));
        user.Password = GetValidPassword("Enter Password:");

        try
        {
            var createdUser = await _userService.CreatedAsync(user);
            AnsiConsole.MarkupLine($"[green]User created successfully with ID {createdUser.Id}[/]");
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
    }

    private async Task ViewAllUsersAsync()
    {
        var users = await _userService.GetAllAsync();
        DisplayUsers(users);
    }

    private void DisplayUsers(IEnumerable<UserViewModel> users)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("First Name");
        table.AddColumn("Last Name");
        table.AddColumn("Phone");
        table.AddColumn("Email");
        table.AddColumn("Role");
        table.AddColumn("Balance");

        foreach (var user in users)
        {
            table.AddRow(user.Id.ToString(), user.FirstName, user.LastName, user.Phone, user.Email, user.Role.ToString(), user.Balance.ToString("C"));
        }

        AnsiConsole.Write(table);
    }

    private async Task UpdateUserAsync()
    {
        var id = AnsiConsole.Ask<long>("Enter the ID of the user to update:");
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
        {
            AnsiConsole.MarkupLine($"[red]User with ID {id} not found.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"[yellow]Updating user with ID {id}[/]");
        AnsiConsole.MarkupLine("[yellow]Leave the field empty if you don't want to update it.[/]");

        user.FirstName = AnsiConsole.Ask("Enter new First Name:", user.FirstName);
        user.LastName = AnsiConsole.Ask("Enter new Last Name:", user.LastName);
        user.Phone = GetPhoneInput("Enter phone number: ");
        user.Email = AnsiConsole.Ask("Enter new Email:", user.Email);
        user.Role = AnsiConsole.Prompt(
            new SelectionPrompt<UserRole>()
                .Title("Select new Role:")
                .PageSize(3)
                .AddChoices(new[]
                {
                    UserRole.Admin,
                    UserRole.Sender
                }));
        user.Password = GetValidPassword("Enter new Password:");

        try
        {
            var updatedUser = await _userService.UpdateAsync(id, user.MapTo<UserUpdateModel>(), false);
            AnsiConsole.MarkupLine($"[green]User updated successfully with ID {updatedUser.Id}[/]");
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
    }
    private string GetValidPassword(string promptMessage)
    {
        string password;
        do
        {
            password = AnsiConsole.Prompt(
                new TextPrompt<string>($"[green]{promptMessage}[/]")
                    .PromptStyle("red")
                    .Secret()).Trim();

            if (password.Length < 4)
            {
                AnsiConsole.MarkupLine("[red1]Password must have at least 4 characters.[/]");
            }
        } while (password.Length < 4);

        return password;
    }

    private async Task DepositAsync()
    {
        var id = AnsiConsole.Ask<long>("Enter the ID of the user to deposit:");
        var amount = AnsiConsole.Ask<decimal>("Enter the deposit amount:");

        try
        {
            var depositedUser = await _userService.DepositAsync(id, amount);
            AnsiConsole.MarkupLine($"[green]Deposit successful for user with ID {depositedUser.Id}[/]");
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
    }

    private async Task DeleteUserAsync()
    {
        var id = AnsiConsole.Ask<long>("Enter the ID of the user to delete:");

        try
        {
            await _userService.DeleteAsync(id);
            AnsiConsole.MarkupLine($"[green]User with ID {id} deleted successfully[/]");
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
    }
    private string GetPhoneInput(string text)
    {
        Console.Write(text);
        string input = Console.ReadLine().Trim();
        while (!Regex.IsMatch(input, @"^(\+998|998|0)([1-9]{1}[0-9]{8})$"))
        {
            Console.WriteLine(text);
            input = Console.ReadLine();
        }
        return input;
    }
}