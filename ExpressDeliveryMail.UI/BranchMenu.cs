using ExpressDeliveryMail.Domain.Entities.Branches;
using ExpressDeliveryMail.Service.Extensions;
using ExpressDeliveryMail.Service.Services;
using Spectre.Console;

namespace ExpressDeliveryMail.UI;

public class BranchMenu
{
    private readonly BranchService _branchService;

    public BranchMenu(BranchService branchService)
    {
        _branchService = branchService;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Branch Menu[/]")
                    .PageSize(4)
                    .AddChoices(new[] { "Create Branch", "View All Branches", "Update Branch", "Delete Branch", "[red]Go Back[/]" }));

            switch (choice)
            {
                case "Create Branch":
                    AnsiConsole.Clear();
                    await CreateBranchAsync();
                    break;
                case "View All Branches":
                    AnsiConsole.Clear();
                    await ViewAllBranchesAsync();
                    break;
                case "Update Branch":
                    AnsiConsole.Clear();
                    await UpdateBranchAsync();
                    break;
                case "Delete Branch":
                    AnsiConsole.Clear();
                    await DeleteBranchAsync();
                    break;
                case "[red]Go Back[/]":
                    return;
            }
        }
    }

    private async Task CreateBranchAsync()
    {
        var branch = new BranchCreationModel();

        branch.Name = AnsiConsole.Ask<string>("Enter Branch Name:");
        branch.Location = AnsiConsole.Ask<string>("Enter Branch Location:");
        branch.Rating = AnsiConsole.Ask<float>("Enter Branch Rating:");

        try
        {
            var createdBranch = await _branchService.CreatedAsync(branch);
            AnsiConsole.MarkupLine($"[green]Branch created successfully with ID {createdBranch.Id}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error creating branch: {ex.Message}[/]");
        }
    }

    private async Task ViewAllBranchesAsync()
    {
        var branches = await _branchService.GetAllAsync();
        DisplayBranches(branches);
    }

    private void DisplayBranches(IEnumerable<BranchViewModel> branches)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Location");
        table.AddColumn("Rating");

        foreach (var branch in branches)
        {
            table.AddRow(branch.Id.ToString(), branch.Name, branch.Location, branch.Rating.ToString());
        }

        AnsiConsole.Write(table);
    }
    private async Task UpdateBranchAsync()
    {
        var id = AnsiConsole.Ask<long>("Enter the ID of the branch to update:");
        var branch = await _branchService.GetByIdAsync(id);

        if (branch == null)
        {
            AnsiConsole.MarkupLine($"[red]Branch with ID {id} not found.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"[yellow]Updating branch with ID {id}[/]");
        AnsiConsole.MarkupLine("[yellow]Leave the field empty if you don't want to update it.[/]");

        branch.Name = AnsiConsole.Ask("Enter new Branch Name:", branch.Name);
        branch.Location = AnsiConsole.Ask("Enter new Branch Location:", branch.Location);
        branch.Rating = branch.Rating;

        try
        {
            var updatedBranch = await _branchService.UpdateAsync(id, branch.MapTo<BranchUpdateModel>(), false);
            AnsiConsole.MarkupLine($"[green]Branch updated successfully with ID {updatedBranch.Id}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error updating branch: {ex.Message}[/]");
        }
    }

    private async Task DeleteBranchAsync()
    {
        var id = AnsiConsole.Ask<long>("Enter the ID of the branch to delete:");

        try
        {
            await _branchService.DeleteAsync(id);
            AnsiConsole.MarkupLine($"[green]Branch with ID {id} deleted successfully[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error deleting branch: {ex.Message}[/]");
        }
    }
}