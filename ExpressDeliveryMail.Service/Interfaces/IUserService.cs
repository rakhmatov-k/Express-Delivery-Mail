using ExpressDeliveryMail.Domain.Entities.Users;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface IUserService
{
    ValueTask<UserViewModel> CreatedAsync(UserCreationModel user);
    ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user, bool IsUsesDeleted);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync();
    ValueTask<UserViewModel> DepositAsync(long id, decimal amount);
}
