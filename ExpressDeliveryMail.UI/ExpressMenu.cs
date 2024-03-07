using ExpressDeliveryMail.Domain.Entities.Expresses;
using ExpressDeliveryMail.Service.Interfaces;
using Spectre.Console;


namespace ExpressDeliveryMail.UI;
public class ExpressMenu
{
    private IExpressService expressService;

    public ExpressMenu(IExpressService expressService)
    {
        this.expressService = expressService;
    }

    public async Task ShowMenuAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[cyan]Express Menu[/]")
                    .PageSize(5)
                    .AddChoices(new[]
                    {
                        "Create Express",
                        "View All Expresses",
                        "Delete Express",
                        "View Express Details",
                        "Exit"
                    }));

            switch (choice)
            {
                case "Create Express":
                    await CreateExpressAsync();
                    break;
                case "View All Expresses":
                    await ViewAllExpressesAsync();
                    break;
                case "Delete Express":
                    await DeleteExpressAsync();
                    break;
                case "View Express Details":
                    await ViewExpressDetailsAsync();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    private async Task CreateExpressAsync()
    {
        var express = new ExpressCreationModel();

        express.BranchId = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Branch ID for the express:")
                .PromptStyle("yellow"));

        express.TransportId = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Transport ID for the express:")
                .PromptStyle("yellow"));

        express.DepartureTime = AnsiConsole.Prompt(
            new TextPrompt<DateTime>("Enter Departure Time for the express:")
                .PromptStyle("yellow"));

        express.Distance = AnsiConsole.Prompt(
            new TextPrompt<decimal>("Enter Distance for the express:")
                .PromptStyle("yellow"));

        try
        {
            var createdExpress = await expressService.CreatedAsync(express);
            AnsiConsole.WriteLine("[green]Express created successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private async Task ViewAllExpressesAsync()
    {
        try
        {
            var expresses = await expressService.GetAllAsync();
            DisplayExpressTable(expresses);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private async Task DeleteExpressAsync()
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Express ID to delete:")
                .PromptStyle("yellow"));

        try
        {
            var result = await expressService.DeleteAsync(id);
            if (result)
                AnsiConsole.WriteLine("[green]Express deleted successfully![/]");
            else
                AnsiConsole.WriteLine("[red]Express not found or could not be deleted.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private async Task ViewExpressDetailsAsync()
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Express ID to view details:")
                .PromptStyle("yellow"));

        try
        {
            var express = await expressService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private void DisplayExpressTable(IEnumerable<ExpressViewModel> expresses)
    {
        var table = new Table();

        table.AddColumn("ID");
        table.AddColumn("Branch ID");
        table.AddColumn("Transport ID");
        table.AddColumn("Departure Time");
        table.AddColumn("Arrival Time");
        table.AddColumn("Distance");

        foreach (var express in expresses)
        {
            table.AddRow(
                express.Id.ToString(),
                express.BranchId.ToString(),
                express.TransportId.ToString(),
                express.DepartureTime.ToString("yyyy-MM-dd HH:mm:ss"),
                express.ArrivalTime.ToString("yyyy-MM-dd HH:mm:ss"),
                express.Distance.ToString());
        }
        AnsiConsole.Write(table);
    }
}
