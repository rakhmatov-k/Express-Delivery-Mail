using ExpressDeliveryMail.Domain.Entities;
using ExpressDeliveryMail.Domain.Enums;
using ExpressDeliveryMail.Service.Interfaces;
using Spectre.Console;

namespace ExpressDeliveryMail.UI;

public class PackageMenu
{
    private IPackageService packageService;

    public PackageMenu(IPackageService packageService)
    {
        this.packageService = packageService;
    }

    public async Task ShowMenuAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Package Menu[/]")
                    .PageSize(5)
                    .AddChoices(new[]
                    {
                        "Create Package",
                        "View All Packages",
                        "Delete Package",
                        "Update Package",
                        "Exit"
                    }));

            switch (choice)
            {
                case "Create Package":
                    await CreatePackageAsync();
                    break;
                case "View All Packages":
                    await ViewAllPackagesAsync();
                    break;
                case "Delete Package":
                    await DeletePackageAsync();
                    break;
                case "Update Package":
                    await UpdatePackageAsync();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    private async Task CreatePackageAsync()
    {
        var package = new PackageCreationModel();

        package.UserId = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter User ID:")
                .PromptStyle("yellow"));

        package.StartBranchId = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Start Branch ID:")
                .PromptStyle("yellow"));

        package.EndBranchId = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter End Branch ID:")
                .PromptStyle("yellow"));

        package.Status = AnsiConsole.Prompt(
            new SelectionPrompt<PackageStatus>()
                .Title("Select Package Status")
                .PageSize(5)
                .AddChoices(new[]
                {
                    PackageStatus.Pending,
                    PackageStatus.InTransit,
                    PackageStatus.Delivered,
                    PackageStatus.FailedDelivery
                }));

        try
        {
            var createdPackage = await packageService.CreatedAsync(package);
            AnsiConsole.WriteLine("[green]Package created successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private async Task ViewAllPackagesAsync()
    {
        try
        {
            var packages = await packageService.GetAllAsync();
            DisplayPackageTable(packages);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private async Task DeletePackageAsync()
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Package ID to delete:")
                .PromptStyle("yellow"));

        try
        {
            var result = await packageService.DeleteAsync(id);
            if (result)
                AnsiConsole.WriteLine("[green]Package deleted successfully![/]");
            else
                AnsiConsole.WriteLine("[red]Package not found or could not be deleted.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private async Task UpdatePackageAsync()
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Package ID to update:")
                .PromptStyle("yellow"));

        var existingPackage = await packageService.GetByIdAsync(id);
        if (existingPackage == null)
        {
            AnsiConsole.WriteLine("[red]Package not found.[/]");
            return;
        }

        var package = new PackageUpdateModel();

        // Get necessary information for package update from the user
        package.UserId = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter User ID:")
                .PromptStyle("yellow")
                .DefaultValue(existingPackage.UserId));

        package.StartBranchId = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Start Branch ID:")
                .PromptStyle("yellow")
                .DefaultValue(existingPackage.StartBranchId));

        package.EndBranchId = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter End Branch ID:")
                .PromptStyle("yellow")
                .DefaultValue(existingPackage.EndBranchId));

        package.Status = AnsiConsole.Prompt(
            new SelectionPrompt<PackageStatus>()
                .Title("Select Package Status")
                .PageSize(5)
                .AddChoices(new[]
                {
                    PackageStatus.Pending,
                    PackageStatus.InTransit,
                    PackageStatus.Delivered,
                    PackageStatus.FailedDelivery
                }));

        try
        {
            var updatedPackage = await packageService.UpdateAsync(id, package, false);
            AnsiConsole.WriteLine("[green]Package updated successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private void DisplayPackageTable(IEnumerable<PackageViewModel> packages)
    {
        var table = new Table();

        table.AddColumn("ID");
        table.AddColumn("User ID");
        table.AddColumn("Start Branch ID");
        table.AddColumn("End Branch ID");
        table.AddColumn("Status");
        table.AddColumn("Category");
        table.AddColumn("Weight");
        table.AddColumn("Receiver Name");
        table.AddColumn("Receiver Phone");

        foreach (var package in packages)
        {
            table.AddRow(
                package.Id.ToString(),
                package.UserId.ToString(),
                package.StartBranchId.ToString(),
                package.EndBranchId.ToString(),
                package.Status.ToString(),
                package.Category.ToString(),
                package.Weight.ToString(),
                package.ReceiverName,
                package.ReceiverPhone);
        }

        AnsiConsole.Write(table);
    }
}
