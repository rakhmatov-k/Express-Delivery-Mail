using ExpressDeliveryMail.Domain.Entities;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface ITransportService
{
    ValueTask<Transport> CreatedAsync(Transport transport);
    ValueTask<Transport> UpdateAsync(long id, Transport transport);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Transport> GetByIdAsync(long id);
    ValueTask<IEnumerable<Transport>> GetAllAsync();
}
