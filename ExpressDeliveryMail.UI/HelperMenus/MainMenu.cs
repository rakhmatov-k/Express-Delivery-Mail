using ExpressDaliveryMail.Data.AppDbContexts;
using ExpressDaliveryMail.Data.Repositories;
using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Service.Services;
using ExpressDeliveryMail.UI.Admin;
using ExpressDeliveryMail.UI.Users;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.Helpers;

public class MainMenu
{
    private MealDbContext mealDbContext;
    private User user;
    private UserService userService;
    private UserActions userActions;
    private UserLogin userLogin;
    private UserRegister userRegister;
    private AdminMenu adminMenu;
    private BranchService branchService;
    private UserMenu userMenu;
    private PackageService packageService;
    private PaymentService paymentService;

    private AdminLogin adminLogin;

    private UserRepositories userRepositories;
    private BranchRepository branchRepository;
    private PackageRepository packageRepository;
    private PaymentRepository paymentRepository;

    public MainMenu()
    {
        userRepositories = new UserRepositories(mealDbContext);
        userService = new UserService(userRepositories);

        branchRepository = new BranchRepository(mealDbContext);
        branchService = new BranchService(branchRepository);

        packageRepository = new PackageRepository(mealDbContext);
        packageService = new PackageService(packageRepository, userService, branchService);

        paymentRepository = new PaymentRepository(mealDbContext);
        paymentService = new PaymentService(userService, paymentRepository);

        userActions = new UserActions(user, userService, branchService, packageService);
        userMenu = new UserMenu(user, userActions, userService, branchService, packageService);
        userLogin = new UserLogin(userService, userActions, branchService, userMenu, packageService);
        userRegister = new UserRegister(userService, userMenu, branchService, paymentService, packageService, userActions);

        adminMenu = new AdminMenu(user, userService, branchService); 
        adminLogin = new AdminLogin(userService, branchService, adminMenu);
    }

    #region Run
    public async Task RunAsync()
    {
        while (true)
        {
            var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Express Delivery Mail[/]")
                    .PageSize(4)
                    .AddChoices(new[] {
                        "As Mail Sender",
                        "As Administrator\n",
                        "[red]Exit[/]"}));

            switch (choise)
            {
                case "As Mail Sender":
                    AnsiConsole.Clear();
                    await CustomerAskAsync();
                    break;
                case "As Administrator\n":
                    AnsiConsole.Clear();
                    await AdminAskAsync();
                    break;
                case "[red]Exit[/]":
                    return;
            }
        }
    }
    #endregion

    #region Customer
    public async Task CustomerAskAsync()
    {
        while (true)
        {
            var c = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("As Mail Sender")
                .PageSize(4)
                .AddChoices(new[] {
                            "Login",
                            "Register\n",
                            "[red]Go Back[/]"}));
            switch (c)
            {
                case "Login":
                    await userLogin.LoginAync();
                    break;
                case "Register\n":
                    await userRegister.RegisterAsync();
                    break;
                case "[red]Go Back[/]":
                    return;
            }
        }
    }
    #endregion

    #region Admin
    public async Task AdminAskAsync()
    {
        while (true)
        {
            var c = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("As Administrator")
                .PageSize(4)
                .AddChoices(new[] {
                            "Login\n",
                            "[red]Go Back[/]"}));
            switch (c)
            {
                case "Login\n":
                    await adminLogin.LoginAsync();
                    break;
                case "[red]Go Back[/]":
                    return;
            }
        }
    }
    #endregion
}