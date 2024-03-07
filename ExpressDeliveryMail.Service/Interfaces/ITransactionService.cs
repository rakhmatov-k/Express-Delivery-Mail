using ExpressDeliveryMail.Domain.Entities.Transactions;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface ITransactionService
{
    ValueTask<TransactionViewModel> CreatedAsync(TransactionCreationModel transaction);
    ValueTask<TransactionViewModel> UpdateAsync(long id, TransactionUpdateModel transaction, bool isUsesDeleted);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<TransactionViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<TransactionViewModel>> GetAllAsync();
}
