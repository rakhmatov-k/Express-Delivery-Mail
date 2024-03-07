using ExpressDeliveryMail.Service.Services;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.Users;

public class UserLogin
{
    private UserService userService;
    private UserActions userActions;
    private UserMenu userMenu;

    public UserLogin(UserService userService, UserActions userActions)
    {
        this.userService = userService;
        this.userActions = userActions;
    }

    #region Login
    public async Task LoginAync()
    {
        AnsiConsole.Clear();
        while (true)
        {
            var username = AnsiConsole.Ask<string>("Enter your [green]username[/]:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter your [green]password[/]:")
                    .PromptStyle("yellow")
                    .Secret());

            try
            {
                //var getCustomer = await userService.GetToLoginAsync(username, password);

                //userMenu = new UserMenu(getCustomer, userActions, userService);
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
