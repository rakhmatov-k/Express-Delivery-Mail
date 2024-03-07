using ExpressDaliveryMail.Data.AppDbContexts;
using ExpressDaliveryMail.Data.Repositories;
using ExpressDeliveryMail.Service.Services;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.MainMenu
{
    public class MainMenu
    {
        private readonly MealDbContext context;

        private readonly UserService _userService;
        private readonly BranchService _branchService;
        private readonly ExpressService _expressService;
        private readonly PackageService _packageService;
        private readonly PaymentService _paymentService;
        private readonly TransactionService _transactionService;
        private readonly TransportService _transportService;

        private readonly UserRepositories userRepositories;
        private readonly BranchRepository branchRepository;
        private readonly ExpressRepository expressRepository;
        private readonly PaymentRepository paymentRepository;
        private readonly PackageRepository packageRepository;
        private readonly TransportRepository transportRepository;
        private readonly TransactionRepository transactionRepository;

        public MainMenu()
        {
            context = new MealDbContext();

            userRepositories = new UserRepositories(context);
            branchRepository = new BranchRepository(context);
            expressRepository = new ExpressRepository(context);
            paymentRepository = new PaymentRepository(context);
            packageRepository = new PackageRepository(context);
            transactionRepository = new TransactionRepository(context);
            transportRepository = new TransportRepository(context);

            _userService = new UserService(userRepositories);
            _branchService = new BranchService(branchRepository);
            _expressService = new ExpressService(expressRepository, _branchService, _transportService);
            _packageService = new PackageService(packageRepository, _userService, _branchService);
            _paymentService = new PaymentService(_userService, paymentRepository);
            _transactionService = new TransactionService(transactionRepository, _expressService, _packageService);
            _transportService = new TransportService(transportRepository);
        }

        public async Task ShowMenuAsync()
        {
            while (true)
            {
                var selectedRole = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select User Role[/]")
                    .PageSize(3)
                    .AddChoices(new[]
                    {
                        "Mail Sender",
                        "Admin"
                    }));
                await Task.Delay(1000);
                AnsiConsole.Clear();
                Console.WriteLine($"[yellow]Welcome to Express Delivery Mail System[/]");

                switch (selectedRole)
                {
                    case "Admin":
                        await ShowAdminMenu();
                        break;
                    case "Mail Sender":
                        await ShowSenderMenu();
                        break;
                    default:
                        Console.WriteLine("[red]Invalid role. Exiting the application.[/]");
                        return;
                }
            }
        }

        private async Task ShowAdminMenu()
        {
            var branchMenu = new BranchMenu(_branchService);
            var expressMenu = new ExpressMenu(_expressService);
            var packageMenu = new PackageMenu(_packageService);
            var paymentMenu = new PaymentMenu(_paymentService);
            var transactionMenu = new TransactionMenu(_transactionService);
            var transportMenu = new TransportMenu(_transportService);
            var userMenu = new UserMenu(_userService);

            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]Admin Menu[/]")
                        .PageSize(7)
                        .AddChoices(new[]
                        {
                            "Branch Menu",
                            "Express Menu",
                            "Package Menu",
                            "Payment Menu",
                            "Transaction Menu",
                            "Transport Menu",
                            "User Menu",
                            "Exit"
                        }));

                switch (choice)
                {
                    case "Branch Menu":
                        await branchMenu.RunAsync();
                        break;
                    case "Express Menu":
                        await expressMenu.ShowMenuAsync();
                        break;
                    case "Package Menu":
                        await packageMenu.ShowMenuAsync();
                        break;
                    case "Payment Menu":
                        await paymentMenu.ShowMenuAsync();
                        break;
                    case "Transaction Menu":
                        await transactionMenu.RunAsync();
                        break;
                    case "Transport Menu":
                        await transportMenu.ShowMenuAsync();
                        break;
                    case "User Menu":
                        await userMenu.RunAsync();
                        break;
                    case "Exit":
                        return;
                }
            }
        }

        private async Task ShowSenderMenu()
        {
            var packageMenu = new PackageMenu(_packageService);
            var paymentMenu = new PaymentMenu(_paymentService);
            var transactionMenu = new TransactionMenu(_transactionService);

            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]Sender Menu[/]")
                        .PageSize(4)
                        .AddChoices(new[]
                        {
                            "Package Menu",
                            "Payment Menu",
                            "Transaction Menu",
                            "Exit"
                        }));

                switch (choice)
                {
                    case "Package Menu":
                        await packageMenu.ShowMenuAsync();
                        break;
                    case "Payment Menu":
                        await paymentMenu.ShowMenuAsync();
                        break;
                    case "Transaction Menu":
                        await transactionMenu.RunAsync();
                        break;
                    case "Exit":
                        return;
                }
            }
        }
    }
}