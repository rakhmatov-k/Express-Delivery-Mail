using ExpressDeliveryMail.Service.Services;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.Admin;

public class AdminLogin
{
    private UserService adminService;
    private AdminMenu adminMenu;

    public AdminLogin(UserService adminService)
    {
        this.adminService = adminService;
    }

    #region Login
    public async Task LoginAsync()
    {
        AnsiConsole.Clear();
        while (true)
        {
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]password[/]:")
            .PromptStyle("yellow")
            .Secret());

            try
            {
                /*var getAdmin = await adminService.LoginAsAdminAsync(password);

                adminMenu = new AdminMenu(getAdmin, adminService);
                await adminMenu.MenuAsync();*/
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