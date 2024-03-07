using ExpressDeliveryMail.Domain.Entities.Transports;
using ExpressDeliveryMail.Domain.Enums;
using ExpressDeliveryMail.Service.Interfaces;
using Spectre.Console;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ExpressDeliveryMail.UI;

public class TransportMenu
{
    private ITransportService transportService;

    public TransportMenu(ITransportService transportService)
    {
        this.transportService = transportService;
    }

    public async Task ShowMenuAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Transport Menu[/]")
                    .PageSize(6)
                    .AddChoices(new[] {
                        "Create Transport",
                        "View All Transports",
                        "Get Transport by ID",
                        "Update Transport",
                        "Delete Transport",
                        "Exit"
                    }));

            switch (choice)
            {
                case "Create Transport":
                    await CreateTransportAsync();
                    break;
                case "View All Transports":
                    await ViewAllTransportsAsync();
                    break;
                case "Get Transport by ID":
                    await GetByIdAndDisplayAsync();
                    break;
                case "Update Transport":
                    await UpdateTransportAsync();
                    break;
                case "Delete Transport":
                    await DeleteTransportAsync();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    private async Task CreateTransportAsync()
    {
        var transport = new TransportCreationModel();

        transport.Type = AnsiConsole.Prompt(
            new SelectionPrompt<TransportType>()
                .Title("Select Transport Type")
                .PageSize(5)
                .AddChoices(new[]
                {
                    TransportType.Truck,
                    TransportType.Plane,
                    TransportType.Ship,
                    TransportType.Bike,
                    TransportType.Car
                }));

        transport.Description = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter Transport Description:")
                .PromptStyle("yellow"));

        try
        {
            var createdTransport = await transportService.CreatedAsync(transport);
            AnsiConsole.WriteLine("[green]Transport created successfully![/]");
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

    private async Task GetByIdAndDisplayAsync()
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Transport ID to view:")
                .PromptStyle("yellow"));

        try
        {
            var transport = await transportService.GetByIdAsync(id);
            DisplayTransportTable(new List<TransportViewModel> { transport });
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private async Task UpdateTransportAsync()
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Transport ID to update:")
                .PromptStyle("yellow"));

        try
        {
            var existingTransport = await transportService.GetByIdAsync(id);
            if (existingTransport == null)
            {
                AnsiConsole.WriteLine("[red]Transport not found.[/]");
                return;
            }

            AnsiConsole.MarkupLine($"[yellow]Updating transport with ID {id}[/]");
            AnsiConsole.MarkupLine("[yellow]Leave the field empty if you don't want to update it.[/]");

            var transportUpdateModel = new TransportUpdateModel();

            transportUpdateModel.Type = AnsiConsole.Prompt(
                new SelectionPrompt<TransportType>()
                    .Title("Select Transport Type")
                    .PageSize(5)
                    .AddChoices(new[]
                    {
                    TransportType.Truck,
                    TransportType.Plane,
                    TransportType.Ship,
                    TransportType.Bike,
                    TransportType.Car
                    }));

            transportUpdateModel.Description = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter Transport Description:")
                    .PromptStyle("yellow")
                    .DefaultValue(existingTransport.Description));

            transportUpdateModel.Colour = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter Transport Colour:")
                    .PromptStyle("yellow")
                    .DefaultValue(existingTransport.Colour));

            var updatedTransport = await transportService.UpdateAsync(id, transportUpdateModel, false);
            AnsiConsole.MarkupLine($"[green]Transport updated successfully with ID {updatedTransport.Id}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }


    private async Task ViewAllTransportsAsync()
    {
        try
        {
            var transports = await transportService.GetAllAsync();
            DisplayTransportTable(transports);
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

    private async Task DeleteTransportAsync()
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Transport ID to delete:")
                .PromptStyle("yellow"));

        try
        {
            var result = await transportService.DeleteAsync(id);
            if (result)
                AnsiConsole.WriteLine("[green]Transport deleted successfully![/]");
            else
                AnsiConsole.WriteLine("[red]Transport not found or could not be deleted.[/]");
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

    private void DisplayTransportTable(IEnumerable<TransportViewModel> transports)
    {
        var table = new Table();

        table.AddColumn("ID");
        table.AddColumn("Type");
        table.AddColumn("Description");
        table.AddColumn("Colour");

        foreach (var transport in transports)
        {
            table.AddRow(transport.Id.ToString(), transport.Type.ToString(), transport.Description, transport.Colour);
        }

        AnsiConsole.Write(table);
    }
}
