//using ExpressDeliveryMail.Data.AppDbContexts;
//using ExpressDeliveryMail.Data.IRepositories;
//using ExpressDeliveryMail.Domain.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace ExpressDeliveryMail.Data.Repositories;

//public class UserRepository : IUserRepository
//{
//    public AppDbContext context;
//    public UserRepository(AppDbContext context)
//    {
//        this.context = context;
//    }

//    public async ValueTask<bool> DeleteAsync(long id)
//    {
//        var existUser = await context.users.FirstAsync(u => u.Id == id && !u.IsDeleted);
//        existUser.IsDeleted = true;
//        existUser.DeletedAt = DateTime.UtcNow;
//        context.SaveChanges();
//        return true;
//    }

//    public async ValueTask<IEnumerable<User>> GetAllAsync()
//    {
//        var users = await context.users.ToListAsync();
//        return users;
//    }

//    public async ValueTask<User> InsertAsync(User user)
//    {
//        var createdUser = await context.users.AddAsync(user);
//        context.SaveChanges();
//        return createdUser.Entity;
//    }

//    public async ValueTask<User> UpdateAsync(long id, User user)
//    {
//        var existUser = await context.users.FirstAsync(u => u.Id == id);
//        existUser.FirstName = user.FirstName;
//        existUser.LastName = user.LastName;
//        existUser.Email = user.Email;
//        existUser.Phone = user.Phone;
//        existUser.Password = user.Password;
//        existUser.Role = user.Role;
//        existUser.Balance = user.Balance;
//        existUser.UpdatedAt = DateTime.UtcNow;
//        existUser.IsDeleted = false;
//        context.SaveChanges();

//        return existUser;
//    }
//}
