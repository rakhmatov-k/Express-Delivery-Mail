using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Service.Services;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.Admin;

public class AdminMenu
{
    private User admin;
    private AdminActions adminActions;
    private UserService userService;
    public AdminMenu(User admin, UserService userService)
    {
        this.admin = admin;
        this.userService = userService;
        adminActions = new AdminActions(admin, userService);
    }

    #region Menu
    public async Task MenuAsync()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Dream[green]House[/][red]/[/]Admin")
                    .PageSize(4)
                    .AddChoices(new[] {
                        "Add new apartment to the Hotel",
                        "Delete apartment from the Hotel",
                        "Get Apartment By ID",
                        "Get All Apartments",
                        "Get All Customers",
                        "View Booked Apartments",
                        "View not Booked Apartments",
                        "View All Econo class Apartments",
                        "View All Normal class Apartments",
                        "View All Premium class Apartments",
                        "View summary balance of Hotel",
                        "Update admin password\n",
                        "[red]Sign out[/]"}));

            switch (choise)
            {
                case "Add new apartment to the Hotel":
                    AnsiConsole.Clear();
                    await adminActions.AddNewApartmentAsync();
                    break;
                case "Delete apartment from the Hotel":
                    AnsiConsole.Clear();
                    await adminActions.DeleteApartmentAsync();
                    break;
                case "Get Apartment By ID":
                    AnsiConsole.Clear();
                    await adminActions.GetApartmentByIdAsync();
                    break;
                case "Get All Apartments":
                    AnsiConsole.Clear();
                    await adminActions.GetAllApartmentsAsync();
                    break;
                case "Get All Customers":
                    AnsiConsole.Clear();
                    await adminActions.GetAllCustomersAsync();
                    break;
                case "View Booked Apartments":
                    AnsiConsole.Clear();
                    await adminActions.BookedApartmentsAsync();
                    break;
                case "View not Booked Apartments":
                    AnsiConsole.Clear();
                    await adminActions.NotBookedApartmentsAsync();
                    break;
                case "View All Econo class Apartments":
                    AnsiConsole.Clear();
                    await adminActions.GetAllEconoApartmentsAsync();
                    break;
                case "View All Normal class Apartments":
                    AnsiConsole.Clear();
                    await adminActions.GetAllNormalApartmentsAsync();
                    break;
                case "View All Premium class Apartments":
                    AnsiConsole.Clear();
                    await adminActions.GetAllPremiumApartmentsAsync();
                    break;
                case "View summary balance of Hotel":
                    AnsiConsole.Clear();
                    await adminActions.HotelBalanceAsync();
                    break;
                case "Update admin password\n":
                    AnsiConsole.Clear();
                    await adminActions.UpdateAdminPasswordAsync();
                    return;
                case "[red]Sign out[/]":
                    return;
            }
        }
    }
    #endregion
}