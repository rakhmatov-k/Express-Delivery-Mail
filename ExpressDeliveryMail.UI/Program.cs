//using ExpressDaliveryMail.Data.AppDbContexts;
//using ExpressDaliveryMail.Data.Repositories;
//using ExpressDeliveryMail.Domain.Entities.Users;
//using ExpressDeliveryMail.Service.Services;
//MealDbContext mealDb = new MealDbContext();
//UserRepositories userRepositories = new UserRepositories(mealDb);

//UserService userService = new UserService(userRepositories);

//UserCreationModel userCreationModel = new UserCreationModel()
//{
//    FirstName ="bdghsds",
//    LastName = "ghdghds",
//    Password ="hjsdh",
//    Phone = "ghdaghsd",
//    Email ="hggwegew"
//};

//await userService.CreatedAsync(userCreationModel);

//using ExpressDaliveryMail.Data.AppDbContexts;
//using ExpressDaliveryMail.Data.Repositories;
//using ExpressDeliveryMail.Domain.Entities.Expresses;
//using ExpressDeliveryMail.Service.Services;

//MealDbContext mealDbContext = new MealDbContext();
//BranchRepository branchRepository = new BranchRepository(mealDbContext);
//TransportRepository transportRepository = new TransportRepository(mealDbContext);
//TransportService transportService = new TransportService(transportRepository);
//BranchService branchService = new BranchService(branchRepository);
//ExpressRepository express = new ExpressRepository(mealDbContext);
//ExpressService expressService = new ExpressService(express, branchService, transportService);
//ExpressCreationModel model = new ExpressCreationModel()
//{
//    Distance = 10,
//    BranchId = 1,
//    TransportId = 1,
//    DepartureTime = DateTime.UtcNow
//};
//await transportService.CreatedAsync(transportCreationModel);
// await expressService.CreatedAsync(model);

using ExpressDeliveryMail.UI.MainMenu;

MainMenu mainMenu = new MainMenu();
await mainMenu.ShowMenuAsync();