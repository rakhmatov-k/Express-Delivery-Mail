using ExpressDeliveryMail.Domain.Entities;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface IExpressService
{
    ValueTask<Express> CreatedAsync(Express express);
    ValueTask<Express> UpdateAsync(long id, Express express);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Express> GetByIdAsync(long id);
    ValueTask<IEnumerable<Express>> GetAllAsync();
}
