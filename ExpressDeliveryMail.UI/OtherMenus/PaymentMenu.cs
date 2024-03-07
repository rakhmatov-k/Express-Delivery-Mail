using ExpressDeliveryMail.Domain.Entities.Payments;
using ExpressDeliveryMail.Domain.Enums;
using ExpressDeliveryMail.Service.Interfaces;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.OtherMenus;

public class PaymentMenu
{
    private IPaymentService paymentService;

    public PaymentMenu(IPaymentService paymentService)
    {
        this.paymentService = paymentService;
    }

    public async Task ShowMenuAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[cyan]Payment Menu[/]")
                    .PageSize(5)
                    .AddChoices(new[]
                    {
                        "Make Payment",
                        "View All Payments",
                        "Delete Payment",
                        "View Payment Details",
                        "Exit"
                    }));

            switch (choice)
            {
                case "Make Payment":
                    await MakePaymentAsync();
                    break;
                case "View All Payments":
                    await ViewAllPaymentsAsync();
                    break;
                case "Delete Payment":
                    await DeletePaymentAsync();
                    break;
                case "View Payment Details":
                    await ViewPaymentDetailsAsync();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    private async Task MakePaymentAsync()
    {
        var payment = new PaymentCreationModel();

        payment.PackageId = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Package ID for the payment:")
                .PromptStyle("yellow"));

        payment.UserId = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter User ID for the payment:")
                .PromptStyle("yellow"));

        payment.Amount = AnsiConsole.Prompt(
            new TextPrompt<decimal>("Enter Payment Amount:")
                .PromptStyle("yellow"));
        payment.Status = PaymentStatus.Pending;

        try
        {
            var createdPayment = await paymentService.CreatedAsync(payment);
            AnsiConsole.WriteLine("[green]Payment made successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private async Task ViewAllPaymentsAsync()
    {
        try
        {
            var payments = await paymentService.GetAllAsync();
            DisplayPaymentTable(payments);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private async Task DeletePaymentAsync()
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Payment ID to delete:")
                .PromptStyle("yellow"));

        try
        {
            var result = await paymentService.DeleteAsync(id);
            if (result)
                AnsiConsole.WriteLine("[green]Payment deleted successfully![/]");
            else
                AnsiConsole.WriteLine("[red]Payment not found or could not be deleted.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private async Task ViewPaymentDetailsAsync()
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<long>("Enter Payment ID to view details:")
                .PromptStyle("yellow"));

        try
        {
            var payment = await paymentService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"[red]Error: {ex.Message}[/]");
        }
    }

    private void DisplayPaymentTable(IEnumerable<PaymentViewModel> payments)
    {
        var table = new Table();

        table.AddColumn("ID");
        table.AddColumn("Package ID");
        table.AddColumn("User ID");
        table.AddColumn("Amount");
        table.AddColumn("Status");

        foreach (var payment in payments)
        {
            table.AddRow(payment.Id.ToString(), payment.PackageId.ToString(), payment.UserId.ToString(), payment.Amount.ToString("C"), payment.Status.ToString());
        }

        AnsiConsole.Write(table);
    }
}