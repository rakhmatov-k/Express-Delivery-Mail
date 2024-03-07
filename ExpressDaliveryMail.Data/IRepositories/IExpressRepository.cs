using ExpressDeliveryMail.Domain.Entities.Expresses;

namespace ExpressDaliveryMail.Data.IRepositories;

public interface IExpressRepository
{
    ValueTask<Express> InsertAsync(Express express);
    ValueTask<Express> UpdateAsync(long id, Express express);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<Express>> GetAllAsync();
}
