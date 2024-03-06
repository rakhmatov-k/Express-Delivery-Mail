using ExpressDaliveryMail.Data.AppDbContexts;
using ExpressDaliveryMail.Data.IRepositories;
using ExpressDeliveryMail.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ExpressDaliveryMail.Data.Repositories;

public class UserRepositories : IUserRepositories
{
    public MealDbContext context;
    public UserRepositories(MealDbContext context)
    {
        this.context = context;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUser = await context.users.FirstAsync(u => u.Id == id && !u.IsDeleted);
        existUser.IsDeleted = true;
        existUser.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync()
    {
        var users = await context.users.ToListAsync();
        return users;
    }

    public async ValueTask<User> InsertAsync(User user)
    {
        var createdUser = await context.users.AddAsync(user);
        context.SaveChanges();
        return createdUser.Entity;
    }

    public async ValueTask<User> UpdateAsync(long id, User user)
    {
        var existUser = await context.users.FirstAsync(u => u.Id == id);
        existUser.Role = user.Role;
        existUser.Email = user.Email;
        existUser.Phone = user.Phone;
        existUser.Balance = user.Balance;
        existUser.LastName = user.LastName;
        existUser.Password = user.Password;
        existUser.FirstName = user.FirstName;
        existUser.UpdatedAt = DateTime.UtcNow;
        existUser.IsDeleted = false;
        context.SaveChanges();

        return existUser;
    }
}
