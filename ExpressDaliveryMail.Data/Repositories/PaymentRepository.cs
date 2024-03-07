using ExpressDaliveryMail.Data.AppDbContexts;
using ExpressDaliveryMail.Data.IRepositories;
using ExpressDeliveryMail.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;

namespace ExpressDaliveryMail.Data.Repositories;

public class PaymentRepository : IPaymentRepository
{
    public MealDbContext context;
    public PaymentRepository(MealDbContext context)
    {
        this.context = context;
    }
    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existPayment = await context.payments.FirstAsync(u => u.Id == id && !u.IsDeleted);
        existPayment.IsDeleted = true;
        existPayment.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }

    public async ValueTask<IEnumerable<Payment>> GetAllAsync()
    {
        var payments = await context.payments.ToListAsync();
        return payments;
    }

    public async ValueTask<Payment> InsertAsync(Payment payment)
    {
        var createdPayment = await context.payments.AddAsync(payment);
        context.SaveChanges();
        return createdPayment.Entity;
    }

    public async ValueTask<Payment> UpdateAsync(long id, Payment payment)
    {
        var existPayment = await context.payments.FirstAsync(u => u.Id == id);
        existPayment.IsDeleted = false;
        existPayment.PackageId = payment.PackageId;
        existPayment.Status = payment.Status;
        existPayment.Amount = payment.Amount;
        existPayment.UserId = payment.UserId;
        existPayment.UpdatedAt = DateTime.UtcNow;
        context.SaveChanges();
        return existPayment;
    }
}