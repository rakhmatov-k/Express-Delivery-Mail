using ExpressDeliveryMail.Domain.Entities;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface IUserService
{
    ValueTask<User> CreatedAsync(User user);
    ValueTask<User> UpdateAsync(long id, User user);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<User> GetByIdAsync(long id);
    ValueTask<IEnumerable<User>> GetAllAsync();
}
