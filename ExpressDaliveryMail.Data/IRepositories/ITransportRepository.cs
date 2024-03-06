using ExpressDeliveryMail.Domain.Entities.Transports;

namespace ExpressDaliveryMail.Data.IRepositories;

public interface ITransportRepository
{
    ValueTask<Transport> InsertAsync(Transport transport);
    ValueTask<Transport> UpdateAsync(long id, Transport transport);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<Transport>> GetAllAsync();
}
