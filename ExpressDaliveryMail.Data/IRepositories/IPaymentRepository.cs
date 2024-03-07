using ExpressDeliveryMail.Domain.Entities.Payments;

namespace ExpressDaliveryMail.Data.IRepositories;

public interface IPaymentRepository
{
    ValueTask<Payment> InsertAsync(Payment payment);
    ValueTask<Payment> UpdateAsync(long id, Payment payment);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<Payment>> GetAllAsync();
}