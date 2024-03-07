using ExpressDeliveryMail.Domain.Entities;
using ExpressDeliveryMail.Domain.Entities.Branches;
using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Service.Extensions;
using ExpressDeliveryMail.Service.Interfaces;
using ExpressDeliveryMail.Service.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace ExpressDeliveryMail.UI.Users;

public class UserActions
{
    private User user;
    private UserService userService;
    private BranchService branchService;
    
    public UserActions(User user, UserService userService, BranchService branchService)
    {
        this.user = user;
        this.userService = userService;
        this.branchService = branchService;
    }

    #region Deposit
    public async Task DepositAsync()
    {
        var amount = AnsiConsole.Ask<decimal>("Enter [green]amount[/]: ");
        await AnsiConsole.Status()
        .Start("Process...", async ctx =>
        {
            AnsiConsole.MarkupLine("loading services...");
            try
            {
                var res = await userService.DepositAsync(user.Id, amount);
                user = res.MapTo<User>();
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine(ex.Message);
            }
            Thread.Sleep(1000);
            AnsiConsole.Clear();
        });
        AnsiConsole.MarkupLine("[green]Done[/] Press any key to continue...");
        Console.ReadLine();
    }
    #endregion

    #region View Profile
    public async Task ViewProfileAsync()
    {
        var userToView = await userService.GetByIdAsync(user.Id);
        var table = new Table();
        
        table.AddColumn("[yellow]Your Profile[/]");

        table.AddRow($"[green]Cusomer ID[/]: {userToView.Id}");
        table.AddRow($"[green]Email[/]: {userToView.Email}");
        table.AddRow($"[green]Balance ($)[/]: {userToView.Balance}");
        table.AddRow($"[green]Firstname[/]: {userToView.FirstName}");
        table.AddRow($"[green]Lastname[/]: {userToView.LastName}");
        table.AddRow($"[green]Phone[/]: {userToView.Phone}");

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("Press any key to exit...");
        Console.ReadLine();
        
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("Press any key to exit...");
        Console.ReadLine();        
    }
    #endregion

    #region Update User Details
    public async Task UpdateUserDetailsAsync()
    {
        UserUpdateModel userUpdate = new UserUpdateModel();

        var username = AnsiConsole.Ask<string>("Enter your [green]username[/]:");
    reenterpassword:
        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your [green]password[/]:")
                .PromptStyle("yellow")
        .Secret());
        while (password.Length < 8)
        {
            AnsiConsole.WriteLine("Password's length must be at least 8 characters");
            goto reenterpassword;
        }
        string email = string.Empty;
        while (String.IsNullOrWhiteSpace(email = AnsiConsole.Ask<string>("Enter your [green]email[/]")) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
        {
            Console.WriteLine("Invalid email address!");
        }
        string firstname = AnsiConsole.Ask<string>("Enter your [green]Firstname[/]");
        string lastname = AnsiConsole.Ask<string>("Enter your [green]Lastname[/]");
        userUpdate.Phone = GetPhoneInput("Enter Phone number: ");
        userUpdate.Password = password;
        userUpdate.FirstName = firstname;
        userUpdate.Email = email;
        userUpdate.FirstName = firstname;
        userUpdate.LastName = lastname;

        try
        {
            await userService.UpdateAsync(user.Id, userUpdate);
            var res = await userService.GetByIdAsync(user.Id);
            user = res.MapTo<User>();
            AnsiConsole.MarkupLine("[green]Success[/] Press any key to continue...");
            Console.ReadLine();
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
    #endregion

    #region helper
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
    #endregion

    #region Delete Account
    public async Task DeleteAccountAsync()
    {
    reenter:
        AnsiConsole.WriteLine($"Are you sure you want to delete your account with username: {user.FirstName}?...");
        AnsiConsole.Write("Press (yes) to confirm, (no) to cancel:");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "yes":
                try
                {
                    AnsiConsole.Clear();
                    await userService.DeleteAsync(user.Id);
                    AnsiConsole.MarkupLine("[green]Success[/]Press any key to exit...");
                    Console.ReadLine();
                    AnsiConsole.Clear();
                    return;
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
            case "no":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Canceled[/]");
                Thread.Sleep(1000);
                return;
            default:
                Console.WriteLine("invalid input");
                await Console.Out.WriteLineAsync("Press any key to reenter...");
                Console.ReadLine();
                goto reenter;
        }
    }
    #endregion

    public async Task VoteForBranchRatingAsync()
    {

    }
    public async Task GetBranchByIdAsync()
    {
        var branchId = AnsiConsole.Ask<long>("Enter [green]Branch ID[/]: ");
        var branch = await branchService.GetByIdAsync(branchId);
        try
        {
            DisplayBranchDetails(branch);
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

    private void DisplayBranchDetails(BranchViewModel branch)
    {
        var table = new Table();
        table.AddColumn("[yellow]Branch Details[/]");

        table.AddRow($"[green]Branch ID[/]: {branch.Id}");
        table.AddRow($"[green]Location[/]: {branch.Location}");
        table.AddRow($"[green]Rating[/]: {branch.Rating}");

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("Press any key to exit...");
    }

    public async Task GetAllBranches()
    {
        AnsiConsole.WriteLine("All branches of Express Delivery Mail: \n");
        var branches = await branchService.GetAllAsync();
        try
        {
            foreach ( var branch in branches )
            {
                DisplayBranchDetails(branch);
            }
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
        Console.ReadLine();
    }

    public async Task DeletePaymentAsync()
    {

    }
    public async Task CreatePaymentAsync()
    {

    }
    public async Task GetAllPackagesAsync()
    {
        //var packages = await packageService.GetAllAsync();
        /*if (packages.Any())
        {
            DisplayAllPackages(packages);
        }
        else
        {
            Console.WriteLine("No packages found.");
        }
        Console.ReadLine();*/
    }

    public async Task GetPackageByIdAsync()
    {
        /*var packageId = AnsiConsole.Ask<long>("Enter [green]package ID[/]: ");
        var package = await packageService.GetByIdAsync(packageId);
        if (package != null)
        {
            DisplayPackageDetails(package);
        }
        else
        {
            Console.WriteLine($"Package with ID {packageId} not found.");
        }
        Console.ReadLine();*/
    }
    private void DisplayAllPackages(IEnumerable<Package> packages)
    {
        var table = new Table();
        table.AddColumn("[yellow]All Packages[/]");
        table.AddColumn("ID");
        table.AddColumn("Sender");
        table.AddColumn("Start Branch");
        table.AddColumn("End Branch");
        table.AddColumn("Status");

        foreach (var package in packages)
        {
            table.AddRow(
                package.Id.ToString(),
                $"{package.User.FirstName} {package.User.LastName}",
                package.StartBranch.Location,
                package.EndBranch.Location,
                package.Status.ToString()
            );
            table.AddEmptyRow(); // Add an empty row for better separation
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("Press any key to exit...");
    }

    private void DisplayPackageDetails(Package package)
    {
        var table = new Table();
        table.AddColumn($"[yellow]Package ID: {package.Id}[/]");
        table.AddColumn("Sender");
        table.AddColumn("Start Branch");
        table.AddColumn("End Branch");
        table.AddColumn("Status");
        table.AddColumn("Category");
        table.AddColumn("Weight");
        table.AddColumn("Receiver Name");
        table.AddColumn("Receiver Phone");

        table.AddRow(
            $"{package.User.FirstName} {package.User.LastName}",
            package.StartBranch.Location,
            package.EndBranch.Location,
            package.Status.ToString(),
            package.Category.ToString(),
            package.Weight.ToString(),
            package.ReceiverName,
            package.ReceiverPhone
        );

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("Press any key to exit...");
    }

    public async Task DeletePackageAsync()
    {

    }
    public async Task UpdatePackageAsync()
    {

    }
    public async Task CreatePackageAsync()
    {

    }
}