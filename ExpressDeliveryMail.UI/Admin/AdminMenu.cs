using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Service.Services;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.Admin;

public class AdminMenu
{
    private User admin;
    private AdminActions adminActions;
    private UserService userService;
    private BranchService branchService;
    public AdminMenu(User admin, UserService userService, BranchService branchService)
    {
        this.admin = admin;
        this.userService = userService;
        this.branchService = branchService;
        adminActions = new AdminActions(admin, userService, branchService);
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
                        "Update admin password\n",
                        "[red]Sign out[/]"}));

            switch (choise)
            {
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