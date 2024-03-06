using ExpressDeliveryMail.Domain.Entities.Transports;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface ITransportService
{
    ValueTask<TransportViewModel> CreatedAsync(TransportCreationModel transport);
    ValueTask<TransportViewModel> UpdateAsync(long id, TransportUpdateModel transport, bool isUsusDeleted);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<TransportViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<TransportViewModel>> GetAllAsync();
}
