using ExpressDeliveryMail.Service.Services;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.Users;

public class UserLogin
{
    private UserService userService;
    private BranchService branchService;
    private UserActions userActions;
    private UserMenu userMenu;
    private PackageService packageService;

    public UserLogin(UserService userService, UserActions userActions, BranchService branchService, UserMenu userMenu, PackageService packageService)
    {
        this.userService = userService;
        this.userActions = userActions;
        this.branchService = branchService;
        this.userMenu = userMenu;
        this.packageService = packageService;
    }

    #region Login
    public async Task LoginAync()
    {
        AnsiConsole.Clear();
        while (true)
        {
            var fName = AnsiConsole.Ask<string>("Enter your [green]First name[/]:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter your [green]password[/]:")
                    .PromptStyle("yellow")
                    .Secret());
            try
            {
                var getCustomer = await userService.LoginUserAsync(fName, password);

                userMenu = new UserMenu(getCustomer, userActions, userService, branchService, packageService);
                await userMenu.MenuAsync();
                return;
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
    }
    #endregion
}
