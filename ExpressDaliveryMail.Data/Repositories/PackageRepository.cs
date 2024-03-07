using ExpressDaliveryMail.Data.AppDbContexts;
using ExpressDaliveryMail.Data.IRepositories;
using ExpressDeliveryMail.Domain.Entities;
using ExpressDeliveryMail.Domain.Entities.Expresses;
using Microsoft.EntityFrameworkCore;

namespace ExpressDaliveryMail.Data.Repositories;

public class PackageRepository : IPackageRepository
{
    public MealDbContext context;
    public PackageRepository(MealDbContext context)
    {
        this.context = context;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existPackage = await context.packages.FirstAsync(u => u.Id == id && !u.IsDeleted);
        existPackage.IsDeleted = true;
        existPackage.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }

    public async ValueTask<IEnumerable<Package>> GetAllAsync()
    {
        var packages = await context.packages.ToListAsync();
        return packages;
    }

    public async ValueTask<Package> InsertAsync(Package package)
    {
        var createdPackage = await context.packages.AddAsync(package);
        context.SaveChanges();
        return createdPackage.Entity;
    }

    public async ValueTask<Package> UpdateAsync(long id, Package package)
    {
        var existPackage = await context.packages.FirstAsync(u => u.Id == id);
        existPackage.IsDeleted = false;
        existPackage.UserId = package.UserId;
        existPackage.Status = package.Status;
        existPackage.Weight = package.Weight;
        existPackage.UpdatedAt = DateTime.UtcNow;
        existPackage.Category = package.Category;
        existPackage.EndBranchId = package.EndBranchId;
        existPackage.ReceiverName = package.ReceiverName;
        existPackage.StartBranchId = package.StartBranchId;
        existPackage.ReceiverPhone = package.ReceiverPhone;
        context.SaveChanges();

        return existPackage;
    }
}