using ExpressDeliveryMail.Domain.Entities.Payments;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface IPaymentService
{
    ValueTask<PaymentViewModel> CreatedAsync(PaymentCreationModel payment);
    ValueTask<PaymentViewModel> UpdateAsync(long id, PaymentUpdateModel payment, bool isUsesDeleted);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<PaymentViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<PaymentViewModel>> GetAllAsync();
}
