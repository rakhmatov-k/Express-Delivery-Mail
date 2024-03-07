using ExpressDeliveryMail.Domain.Entities.Expresses;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface IExpressService
{
    ValueTask<ExpressViewModel> CreatedAsync(ExpressCreationModel express);
    ValueTask<ExpressViewModel> UpdateAsync(long id, ExpressUpdateModel express, bool isUsesDeleted);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<ExpressViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<ExpressViewModel>> GetAllAsync();
}
