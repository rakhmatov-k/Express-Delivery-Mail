using ExpressDaliveryMail.Data.Repositories;
using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Domain.Enums;
using ExpressDeliveryMail.Service.Extensions;
using ExpressDeliveryMail.Service.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ExpressDeliveryMail.Service.Services;

public class UserService : IUserService
{
    private readonly UserRepositories userRepostories;
    public UserService(UserRepositories userRepostories)
    {
        this.userRepostories = userRepostories;
    }

    public async ValueTask<UserViewModel> CreatedAsync(UserCreationModel user)
    {
        var users = await userRepostories.GetAllAsync();
        var existUser = users.FirstOrDefault(u => u.Email == user.Email);
        if (existUser != null)
        {
            if (existUser.IsDeleted)
                return await UpdateAsync(existUser.Id, user.MapTo<UserUpdateModel>(), true);
            throw new Exception($"This user is already exist With this email : {user.Email}");
        }
        var createdUser = await userRepostories.InsertAsync(user.MapTo<User>());
        return createdUser.MapTo<UserViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var users = await userRepostories.GetAllAsync();
        var existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This user is not found With this id {id}");

        await userRepostories.DeleteAsync(id);
        return true;
    }

    public async ValueTask<UserViewModel> DepositAsync(long id, decimal amount)
    {
        var users = await userRepostories.GetAllAsync();
        var existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This user is not found With this id {id}");

        existUser.Balance += amount;
        var depositUser = await userRepostories.UpdateAsync(id, existUser);
        return depositUser.MapTo<UserViewModel>();
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync()
    {
        var users = await userRepostories.GetAllAsync();
        return users.MapTo<UserViewModel>();
    }

    public async ValueTask<UserViewModel> GetByIdAsync(long id)
    {
        var users = await userRepostories.GetAllAsync();
        var existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This user is not found With this id {id}");

        return existUser.MapTo<UserViewModel>();
    }

    public async ValueTask<User> LoginAsync(string password)
    {
        var users = await userRepostories.GetAllAsync();
        var user = users.FirstOrDefault(a => a.Role == UserRole.Admin && VerifyPassword(a.Password, password))
            ?? throw new Exception("Password is not match.");
        return user;
    }
    public async ValueTask<User> LoginUserAsync(string FirstName, string password)
    {
        var users = await userRepostories.GetAllAsync();
        var user = users.FirstOrDefault(a => a.Role == UserRole.Sender && a.FirstName == FirstName && VerifyPassword(a.Password, password))
            ?? throw new Exception("Password is not match.");
        return user;
    }

    public async ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user, bool isUsesDeleted = false)
    {
        var users = await userRepostories.GetAllAsync();
        var existUser = new User();
        if (isUsesDeleted)
            existUser = users.First(u => u.Id == id);
        else
            existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
                ?? throw new Exception($"This user is not found With this id {id}");

        var updatedUser = await userRepostories.UpdateAsync(id, user.MapTo<User>());
        return updatedUser.MapTo<UserViewModel>();
    }
    private string Hashing(string password)
    {
        using (SHA256 hash = SHA256.Create())
        {
            byte[] hashedBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
    private bool VerifyPassword(string actualHashedPassword, string enteredPassword)
    {
        string enteredHashedPassword = Hashing(enteredPassword);
        return actualHashedPassword == enteredHashedPassword;
    }
}