using ExpressDaliveryMail.Data.Repositories;
using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Service.Services;
using ExpressDeliveryMail.UI.Admin;
using ExpressDeliveryMail.UI.Users;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.Helpers;

public class MainMenu
{
    private User user;
    private UserService userService;
    private UserActions userActions;
    private TransportService transportService;
    private BranchService branchService;

    private AdminLogin adminLogin;

    private UserLogin userLogin;
    private UserRegister userRegister;

    private UserRepositories userRepositories;

    public MainMenu()
    {
        user = new User();
        userService = new UserService(userRepositories);
        userActions = new UserActions(user, userService, branchService);
        adminLogin = new AdminLogin(userService);
        userLogin = new UserLogin(userService, userActions);
        userRegister = new UserRegister(userService);
    }

    #region Run
    public async Task RunAsync()
    {
        while (true)
        {
            var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Dream[green]House[/]")
                    .PageSize(4)
                    .AddChoices(new[] {
                        "As Customer",
                        "As Administrator\n",
                        "[red]Exit[/]"}));

            switch (choise)
            {
                case "As Customer":
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
                .Title("As Customer")
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