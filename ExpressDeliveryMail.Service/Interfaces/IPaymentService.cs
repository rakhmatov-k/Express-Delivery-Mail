using ExpressDeliveryMail.Domain.Entities;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface IPaymentService
{
    ValueTask<Payment> CreatedAsync(Payment payment);
    ValueTask<Payment> UpdateAsync(long id, Payment payment);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Payment> GetByIdAsync(long id);
    ValueTask<IEnumerable<Payment>> GetAllAsync();
}
