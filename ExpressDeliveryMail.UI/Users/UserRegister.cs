using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Domain.Enums;
using ExpressDeliveryMail.Service.Services;
using ExpressDeliveryMail.UI.HelperMenus;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace ExpressDeliveryMail.UI.Users;

public class UserRegister
{
    private UserService userService;
    private UserMenu userMenu;
    private BranchService branchService;
    private PaymentService paymentService;
    private PackageService packageService;
    private UserActions userActions;

    public UserRegister(UserService userService, UserMenu userMenu, BranchService branchService, PaymentService paymentService, PackageService packageService, UserActions userActions)
    {
        this.userService = userService;
        this.paymentService = paymentService;
        this.userMenu = userMenu;
        this.branchService = branchService;
        this.packageService = packageService;
        this.userActions = userActions;
    }

    #region Registration
    public async Task RegisterAsync()
    {
        AnsiConsole.Clear();
        while (true)
        {
            var userCreationModel = new UserCreationModel();

            var username = AnsiConsole.Ask<string>("Enter your [green]username[/]:");
        reenterpassword:
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter your [green]password[/]:")
                    .PromptStyle("yellow")
            .Secret());
            while (password.Length < 8)
            {
                AnsiConsole.WriteLine("Password's length must be at least 8 characters");
                goto reenterpassword;
            }
            string email = string.Empty;
            while (String.IsNullOrWhiteSpace(email = AnsiConsole.Ask<string>("Enter your [green]email[/]")) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                Console.WriteLine("Invalid email address!");
            }
            string firstname = AnsiConsole.Ask<string>("Enter your [green]Firstname[/]");
            string lastname = AnsiConsole.Ask<string>("Enter your [green]Lastname[/]");

            var HashedPassword = PasswordActions.Hashing(password);

            userCreationModel.Password = HashedPassword;
            userCreationModel.Email = email;
            userCreationModel.FirstName = firstname;
            userCreationModel.LastName = lastname;
            userCreationModel.Role = UserRole.Sender;

            try
            {
                var createdUser = await userService.CreatedAsync(userCreationModel);
                var getUser = await userService.LoginUserAsync(username, password);

                userMenu = new UserMenu(getUser, userActions, userService, branchService, packageService);
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
