using ExpressDeliveryMail.Domain.Entities;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface ITransactionService
{
    ValueTask<Transaction> CreatedAsync(Transaction transaction);
    ValueTask<Transaction> UpdateAsync(long id, Transaction transaction);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Transaction> GetByIdAsync(long id);
    ValueTask<IEnumerable<Transaction>> GetAllAsync();
}
