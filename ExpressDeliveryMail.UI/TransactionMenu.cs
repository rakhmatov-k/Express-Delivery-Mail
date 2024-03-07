using ExpressDeliveryMail.Domain.Entities.Transactions;
using ExpressDeliveryMail.Service.Extensions;
using Spectre.Console;

namespace ExpressDeliveryMail.UI;

public class TransactionMenu
{
    private readonly TransactionService _transactionService;

    public TransactionMenu(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Transaction Menu[/]")
                    .PageSize(5)
                    .AddChoices(new[] { "Create Transaction", "View All Transactions", "Update Transaction", "Delete Transaction", "[red]Go Back[/]" }));

            switch (choice)
            {
                case "Create Transaction":
                    AnsiConsole.Clear();
                    await CreateTransactionAsync();
                    break;
                case "View All Transactions":
                    AnsiConsole.Clear();
                    await ViewAllTransactionsAsync();
                    break;
                case "Update Transaction":
                    AnsiConsole.Clear();
                    await UpdateTransactionAsync();
                    break;
                case "Delete Transaction":
                    AnsiConsole.Clear();
                    await DeleteTransactionAsync();
                    break;
                case "[red]Go Back[/]":
                    return;
            }
        }
    }

    private async Task CreateTransactionAsync()
    {
        var transaction = new TransactionCreationModel();

        transaction.ExpressId = AnsiConsole.Ask<long>("Enter Express ID:");
        transaction.PackageId = AnsiConsole.Ask<long>("Enter Package ID:");

        try
        {
            var createdTransaction = await _transactionService.CreatedAsync(transaction);
            AnsiConsole.MarkupLine($"[green]Transaction created successfully with ID {createdTransaction.Id}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error creating transaction: {ex.Message}[/]");
        }
    }

    private async Task ViewAllTransactionsAsync()
    {
        var transactions = await _transactionService.GetAllAsync();
        DisplayTransactions(transactions);
    }

    private void DisplayTransactions(IEnumerable<TransactionViewModel> transactions)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Express ID");
        table.AddColumn("Package ID");

        foreach (var transaction in transactions)
        {
            table.AddRow(transaction.Id.ToString(), transaction.ExpressId.ToString(), transaction.PackageId.ToString());
        }

        AnsiConsole.Render(table);
    }

    private async Task UpdateTransactionAsync()
    {
        var id = AnsiConsole.Ask<long>("Enter the ID of the transaction to update:");
        var transaction = await _transactionService.GetByIdAsync(id);

        if (transaction == null)
        {
            AnsiConsole.MarkupLine($"[red]Transaction with ID {id} not found.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"[yellow]Updating transaction with ID {id}[/]");
        AnsiConsole.MarkupLine("[yellow]Leave the field empty if you don't want to update it.[/]");

        transaction.ExpressId = AnsiConsole.Ask("Enter new Express ID:", transaction.ExpressId);
        transaction.PackageId = AnsiConsole.Ask("Enter new Package ID:", transaction.PackageId);

        try
        {
            var updatedTransaction = await _transactionService.UpdateAsync(id, transaction.MapTo<TransactionUpdateModel>(), false);
            AnsiConsole.MarkupLine($"[green]Transaction updated successfully with ID {updatedTransaction.Id}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error updating transaction: {ex.Message}[/]");
        }
    }

    private async Task DeleteTransactionAsync()
    {
        var id = AnsiConsole.Ask<long>("Enter the ID of the transaction to delete:");

        try
        {
            await _transactionService.DeleteAsync(id);
            AnsiConsole.MarkupLine($"[green]Transaction with ID {id} deleted successfully[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error deleting transaction: {ex.Message}[/]");
        }
    }
}