using ExpressDeliveryMail.Domain.Entities.Branches;
using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Service.Interfaces;
using ExpressDeliveryMail.Service.Services;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.Admin;

public class AdminActions
{
    public AdminActions(User admin, UserService userService, BranchService branchService)
    {
        this.admin = admin;
        this.userService = userService;
        this.branchService = branchService;
    }

    private User admin;
    private UserService userService;
    private BranchService branchService;

    #region Add new Branch
    public async Task AddNewBranchAsync()
    {
        BranchCreationModel model = new BranchCreationModel();
        var name = AnsiConsole.Ask<string>("Enter [green]Name[/]:");
        var location = AnsiConsole.Ask<string>("Enter [green]Location[/]:");

        model.Name = name;
        model.Location = location;
        model.Rating = 0;

        try
        {
            BranchViewModel created = await branchService.CreatedAsync(model);

            var table = new Table();
            table.AddColumn("[yellow]Created Branch[/]");
            table.AddRow($"[green]Branch ID[/]: {created.Id}");
            table.AddRow($"[green]Name[/]: {created.Name}");
            table.AddRow($"[green]Location[/]: {created.Location}");
            table.AddRow($"[green]Rating[/]: {created.Rating}");

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            AnsiConsole.WriteLine("Press any key to exit and try again.");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
    }
    #endregion

    #region Delete Branch
    public async Task DeleteBranchAsync()
    {
        var id = AnsiConsole.Ask<int>("Enter [green]BranchID[/]:");
        try
        {
            AnsiConsole.Clear();
            await branchService.DeleteAsync(id);
            AnsiConsole.MarkupLine("[green]Branch Deleted[/] Press enter to exit...");
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
    }
    #endregion

    #region Get Branch by ID
    public async Task GetBranchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Enter [green]BranchID[/]:");
        try
        {
            AnsiConsole.Clear();
            var branch = await branchService.GetByIdAsync(id);
            var table = new Table();

            table.AddColumn("[yellow]Branch[/]");
            table.AddRow($"[green]Branch ID[/]: {branch.Id}");
            table.AddRow($"[green]Name[/]: {branch.Name}");
            table.AddRow($"[green]Location[/]: {branch.Location}");
            table.AddRow($"[green]Rating[/]: {branch.Rating}");

            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("Press enter to exit...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            AnsiConsole.WriteLine("Press any enter to exit...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
    }
    #endregion

    #region Update Branch Information
    public async Task UpdateBranchAsync()
    {
        var id = AnsiConsole.Ask<int>("Enter [green]BranchID[/]:");

        try
        {
            var existingBranch = await branchService.GetByIdAsync(id);

            var updatedName = AnsiConsole.Ask<string>($"Enter new [green]Name[/] ({existingBranch.Name}): ", existingBranch.Name);
            var updatedLocation = AnsiConsole.Ask<string>($"Enter new [green]Location[/] ({existingBranch.Location}): ", existingBranch.Location);
            var updatedRating = AnsiConsole.Ask<float>($"Enter new [green]Rating[/] ({existingBranch.Rating}): ", existingBranch.Rating);

            var updateModel = new BranchUpdateModel
            {
                Name = updatedName,
                Location = updatedLocation,
                Rating = updatedRating
            };

            var updatedBranch = await branchService.UpdateAsync(id, updateModel, false);

            var table = new Table();
            table.AddColumn("[yellow]Updated Branch Information[/]");
            table.AddRow($"[green]Branch ID[/]: {updatedBranch.Id}");
            table.AddRow($"[green]Name[/]: {updatedBranch.Name}");
            table.AddRow($"[green]Location[/]: {updatedBranch.Location}");
            table.AddRow($"[green]Rating[/]: {updatedBranch.Rating}");

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            AnsiConsole.WriteLine("Press any key to exit and try again.");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
    }
    #endregion

    #region Update Admin Password
    public async Task UpdateAdminPasswordAsync()
    {
        var adminmodel = new User();
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
        try
        {
            /*adminmodel.Password = password;
            admin = await userService.UpdateAsync(adminmodel.Id, adminmodel);
            AnsiConsole.MarkupLine("[green]Password Changed[/] Press enter to exit...");
            Console.ReadLine();
            AnsiConsole.Clear();*/
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

    }
    #endregion
}