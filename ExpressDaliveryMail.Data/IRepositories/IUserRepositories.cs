using ExpressDeliveryMail.Domain.Entities.Users;

namespace ExpressDaliveryMail.Data.IRepositories;

public interface IUserRepositories
{
    ValueTask<User> InsertAsync(User user);
    ValueTask<User> UpdateAsync(long id, User user);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<User>> GetAllAsync();
}
