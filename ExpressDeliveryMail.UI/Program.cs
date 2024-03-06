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
//using ExpressDeliveryMail.Domain.Entities.Branches;
//using ExpressDeliveryMail.Service.Services;

//MealDbContext mealDb = new MealDbContext();
//BranchRepository branchRepository = new BranchRepository(mealDb);

//BranchService branch = new BranchService(branchRepository);

//BranchCreationModel branchCreationModel = new BranchCreationModel()
//{
//    Name = "name",
//    Location = "dfdf",
//    Rating = 6
//};
//await branch.CreatedAsync(branchCreationModel);