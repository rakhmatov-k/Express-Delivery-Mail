using ExpressDaliveryMail.Data.AppDbContexts;
using ExpressDaliveryMail.Data.IRepositories;
using ExpressDeliveryMail.Domain.Entities;
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
        existPackage.Weight = package.Weight;
        existPackage.ReceiverPhone = package.ReceiverPhone;
        existPackage.ReceiverName = package.ReceiverName;
        existPackage.Status = package.Status;
        existPackage.Category = package.Category;
        existPackage.EndBranch = package.EndBranch;
        existPackage.StartBranch = package.StartBranch;
        existPackage.StartBranchId = package.StartBranchId;
        existPackage.EndBranchId = package.EndBranchId;
        existPackage.User = package.User;
        existPackage.UserId = package.UserId;   
        existPackage.UpdatedAt = DateTime.UtcNow;
        context.SaveChanges();

        return existPackage;
    }
}