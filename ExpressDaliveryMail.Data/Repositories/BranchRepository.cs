using ExpressDaliveryMail.Data.AppDbContexts;
using ExpressDaliveryMail.Data.IRepositories;
using ExpressDeliveryMail.Domain.Entities.Branches;
using Microsoft.EntityFrameworkCore;

namespace ExpressDaliveryMail.Data.Repositories;

public class BranchRepository : IBranchRepostory
{
    public MealDbContext context;
    public BranchRepository(MealDbContext context)
    {
        this.context = context;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existBranch = await context.branches.FirstAsync(u => u.Id == id && !u.IsDeleted);
        existBranch.IsDeleted = true;
        existBranch.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }

    public async ValueTask<IEnumerable<Branch>> GetAllAsync()
    {
        var branches = await context.branches.ToListAsync();
        return branches;
    }

    public async ValueTask<Branch> InsertAsync(Branch branch)
    {
        var createdBranch = await context.branches.AddAsync(branch);
        context.SaveChanges();
        return createdBranch.Entity;
    }

    public async ValueTask<Branch> UpdateAsync(long id, Branch branch)
    {
        var existBranch = await context.branches.FirstAsync(u => u.Id == id);
        existBranch.IsDeleted = false;
        existBranch.Name = branch.Name;
        existBranch.Rating = branch.Rating;
        existBranch.Location = branch.Location;
        existBranch.UpdatedAt = DateTime.UtcNow;
        context.SaveChanges();

        return existBranch;
    }
}
