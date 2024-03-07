using ExpressDaliveryMail.Data.Repositories;
using ExpressDeliveryMail.Domain.Entities.Payments;
using ExpressDeliveryMail.Domain.Enums;
using ExpressDeliveryMail.Service.Extensions;
using ExpressDeliveryMail.Service.Interfaces;

namespace ExpressDeliveryMail.Service.Services;

public class PaymentService : IPaymentService
{
    private PaymentRepository paymentRepository;
    private UserService userService;
    public PaymentService(UserService userService, PaymentRepository paymentRepository)
    {
        this.userService = userService;
        this.paymentRepository = paymentRepository;
    }
    public async ValueTask<PaymentViewModel> CreatedAsync(PaymentCreationModel payment)
    {
        var packages = await paymentRepository.GetAllAsync();
        var existPackage = packages.FirstOrDefault(p => p.PackageId == payment.PackageId);
        var existUser = await userService.GetByIdAsync(payment.UserId);

        if (existPackage == null || existUser == null)
        {
            throw new Exception("Invalid package or user.");
        }

        var payments = await paymentRepository.GetAllAsync();
        var existCompletedPayment = payments.FirstOrDefault(p => p.Status == PaymentStatus.Completed);

        if (existCompletedPayment != null)
        {
            if (existCompletedPayment.IsDeleted)
            {
                return await UpdateAsync(existCompletedPayment.Id, payment.MapTo<PaymentUpdateModel>(), true);
            }

            throw new Exception($"Payment with status 'Completed' already exists with ID {existCompletedPayment.Id}");
        }

        var createdPayment = await paymentRepository.InsertAsync(payment.MapTo<Payment>());

        return createdPayment.MapTo<PaymentViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var payments = await paymentRepository.GetAllAsync();
        var payment = payments.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception("This payment is not found with this id {id}");

        payment.IsDeleted = true;
        payment.DeletedAt = DateTime.UtcNow;

        await paymentRepository.UpdateAsync(id, payment);

        return true;
    }

    public async ValueTask<IEnumerable<PaymentViewModel>> GetAllAsync()
    {
        var payments = await paymentRepository.GetAllAsync();
        return payments.Where(p => !p.IsDeleted).MapTo<PaymentViewModel>();
    }

    public async ValueTask<PaymentViewModel> GetByIdAsync(long id)
    {
        var payments = await paymentRepository.GetAllAsync();
        var payment = payments.FirstOrDefault(p => p.Id == id && p.IsDeleted)
            ?? throw new Exception($"This payment is not found wiith this Id {id}");

        return payment.MapTo<PaymentViewModel>();
    }

    public async ValueTask<PaymentViewModel> UpdateAsync(long id, PaymentUpdateModel payment, bool isUsesDeleted)
    {
        var payments = await paymentRepository.GetAllAsync();
        var existingPayment = payments.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"This payment is not found with this Id {id}");

        if (existingPayment == null || isUsesDeleted)
        {
            throw new Exception($"Payment with ID {id} not found.");
        }

        var updatedPayment = await paymentRepository.UpdateAsync(id, payment.MapTo<Payment>());

        return updatedPayment.MapTo<PaymentViewModel>();
    }
}