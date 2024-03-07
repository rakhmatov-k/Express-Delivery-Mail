using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Service.Interfaces;
using ExpressDeliveryMail.Service.Services;
using Spectre.Console;

namespace ExpressDeliveryMail.UI.Users
{
    public class UserMenu
    {
        private User user;
        private UserActions userActions;
        private UserService userService;
        private BranchService branchService;
        private PackageService packageService;

        public UserMenu(User user, UserActions userActions, UserService userService, BranchService branchService, PackageService packageService)
        {
            this.user = user;
            this.userService = userService;
            this.branchService = branchService;
            userActions = new UserActions(user, userService, branchService, packageService);
            this.packageService = packageService;
        }

        public async Task MenuAsync()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var choice = AnsiConsole.Prompt(
                   new SelectionPrompt<string>()
                       .Title("Dream[green]House[/][red]/[/]User")
                       .PageSize(14)
                       .AddChoices(new[] {
                            "View Profile",
                            "Update User details",
                            "Deposit",
                            "Delete account",
                            "Sent Package",
                            "Update Package",
                            "Delete Package",
                            "Get Package by ID",
                            "Get All Packages",
                            "Make Payment",
                            "Cancel Payment",
                            "View All Branches",
                            "View Branch by ID",
                            "Vote for Branch Rating",
                            "[red]Sign out[/]"}));

                switch (choice)
                {
                    case "Deposit":
                        AnsiConsole.Clear();
                        await userActions.DepositAsync();
                        break;                    
                    case "View Profile":
                        AnsiConsole.Clear();
                        await userActions.ViewProfileAsync();
                        break;
                    case "Update User details":
                        AnsiConsole.Clear();
                        await userActions.UpdateUserDetailsAsync();
                        break;
                    case "Delete account":
                        AnsiConsole.Clear();
                        await userActions.DeleteAccountAsync();
                        return;
                    case "Sent Package":
                        await userActions.CreatePackageAsync();
                        break;
                    case "Update Package":
                        await userActions.UpdatePackageAsync();
                        break;
                    case "Delete Package":
                        await userActions.DeletePackageAsync();
                        break;
                    case "Get Package by ID":
                        await userActions.GetPackageByIdAsync();
                        break;
                    case "Get All Packages":
                        await userActions.GetAllPackagesAsync();
                        break;
                    case "Make Payment":
                        await userActions.CreatePaymentAsync();
                        break;
                    case "Cancel Payment":
                        await userActions.DeletePaymentAsync();
                        break;
                    case "View All Branches":
                        await userActions.GetAllBranches();
                        break;
                    case "View Branch by ID":
                        await userActions.GetBranchByIdAsync();
                        break;
                    case "Vote for Branch Rating":
                        await userActions.VoteForBranchRatingAsync();
                        break;
                    case "[red]Sign out[/]":
                        return;
                }
            }
        }
    }
}
