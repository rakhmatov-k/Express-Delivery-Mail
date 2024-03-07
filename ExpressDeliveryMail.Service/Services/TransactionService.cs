using ExpressDeliveryMail.Domain.Entities;
using ExpressDeliveryMail.Service.Interfaces;

namespace ExpressDeliveryMail.Service.Services;

public class TransactionService : ITransactionService
{
    public ValueTask<Transaction> CreatedAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<Transaction>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public ValueTask<Transaction> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Transaction> UpdateAsync(long id, Transaction transaction)
    {
        throw new NotImplementedException();
    }
}