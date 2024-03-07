using ExpressDaliveryMail.Data.AppDbContexts;
using ExpressDaliveryMail.Data.IRepositories;
using ExpressDeliveryMail.Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ExpressDaliveryMail.Data.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public MealDbContext context;
    public TransactionRepository(MealDbContext context)
    {
        this.context = context;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTransaction = await context.transactions.FirstAsync(u => u.Id == id && !u.IsDeleted);
        existTransaction.IsDeleted = true;
        existTransaction.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }

    public async ValueTask<IEnumerable<Transaction>> GetAllAsync()
    {
        var transactions = await context.transactions.ToListAsync();
        return transactions;
    }

    public async ValueTask<Transaction> InsertAsync(Transaction transaction)
    {
        var createdTransaction = await context.transactions.AddAsync(transaction);
        context.SaveChanges();
        return createdTransaction.Entity;
    }

    public async ValueTask<Transaction> UpdateAsync(long id, Transaction transaction)
    {
        var existTransaction = await context.transactions.FirstAsync(u => u.Id == id);
        existTransaction.IsDeleted = false;
        existTransaction.UpdatedAt = DateTime.UtcNow;
        existTransaction.PackageId = transaction.PackageId;
        existTransaction.ExpressId = transaction.ExpressId;
        context.SaveChanges();

        return existTransaction;
    }
}
