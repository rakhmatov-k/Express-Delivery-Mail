using ExpressDaliveryMail.Data.Repositories;
using ExpressDeliveryMail.Domain.Entities.Transactions;
using ExpressDeliveryMail.Service.Extensions;
using ExpressDeliveryMail.Service.Services;

public class TransactionService 
{
    private TransactionRepository transactionRepository;
    private ExpressService expressService;
    private PackageService packageService;
    public TransactionService(TransactionRepository transactionRepository, ExpressService expressService, PackageService packageService)
    {
        this.transactionRepository = transactionRepository;
        this.expressService = expressService;
        this.packageService = packageService;
    }

    public async ValueTask<TransactionViewModel> CreatedAsync(TransactionCreationModel transaction)
    {
        var existExpress = await expressService.GetByIdAsync(transaction.ExpressId);
        var existPackage = await packageService.GetByIdAsync(transaction.PackageId);

        var transactions = await transactionRepository.GetAllAsync();
        var existTransaction = transactions.FirstOrDefault(e => e.ExpressId == transaction.ExpressId && 
                                                        e.PackageId == transaction.PackageId);
        if (existTransaction is not null)
        {
            if (existTransaction.IsDeleted)
                return await UpdateAsync(existTransaction.Id, transaction.MapTo<TransactionUpdateModel>(), true);
            throw new Exception($"This transactin is already exist With this express id {transaction.ExpressId} and package id {transaction.PackageId}");
        }

        var createdTransaction = await transactionRepository.InsertAsync(transaction.MapTo<Transaction>());
        return createdTransaction.MapTo<TransactionViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var transactions = await transactionRepository.GetAllAsync();
        var existExpress = transactions.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
            ?? throw new Exception($"This transaction is not found With this id {id}");

        await transactionRepository.DeleteAsync(id);
        return true;
    }

    public async ValueTask<IEnumerable<TransactionViewModel>> GetAllAsync()
    {
        var transactions = await transactionRepository.GetAllAsync();
        return transactions.Where(a => !a.IsDeleted).MapTo<TransactionViewModel>();
    }

    public async ValueTask<TransactionViewModel> GetByIdAsync(long id)
    {
        var transactions = await transactionRepository.GetAllAsync();
        var existTransaction = transactions.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
            ?? throw new Exception($"This transaction is not found With this id {id}");

        return existTransaction.MapTo<TransactionViewModel>();
    }

    public async ValueTask<TransactionViewModel> UpdateAsync(long id, TransactionUpdateModel transaction, bool isUsesDeleted)
    {
        var existExpress = await expressService.GetByIdAsync(transaction.ExpressId);
        var existPackage = await packageService.GetByIdAsync(transaction.PackageId);

        var transactions = await transactionRepository.GetAllAsync();
        var existTransaction = new Transaction();
        if (isUsesDeleted)
            existTransaction = transactions.First(a => a.Id == id);
        else
            existTransaction = transactions.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
                ?? throw new Exception($"This transaction is not found With this id {id}");

        var updatedTransaction = await transactionRepository.UpdateAsync(id, transaction.MapTo<Transaction>());
        return updatedTransaction.MapTo<TransactionViewModel>();
    }
}
