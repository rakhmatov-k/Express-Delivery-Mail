using ExpressDeliveryMail.Domain.Entities.Branches;
using ExpressDeliveryMail.Domain.Entities.Transactions;

namespace ExpressDaliveryMail.Data.IRepositories;

public interface ITransactionRepository
{
    ValueTask<Transaction> InsertAsync(Transaction transaction);
    ValueTask<Transaction> UpdateAsync(long id, Transaction transaction);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<Transaction>> GetAllAsync();
}
