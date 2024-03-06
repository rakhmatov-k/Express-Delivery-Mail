using ExpressDaliveryMail.Data.IRepositories;
using ExpressDaliveryMail.Data.Repositories;
using ExpressDeliveryMail.Domain.Entities.Branches;
using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Service.Extensions;
using ExpressDeliveryMail.Service.Interfaces;

namespace ExpressDeliveryMail.Service.Services;

public class BranchService : IBranchService
{
    private BranchRepository branchRepository;
    public BranchService(BranchRepository branchRepository)
    {
        this.branchRepository = branchRepository;
    }
    public async ValueTask<BranchViewModel> CreatedAsync(BranchCreationModel branch)
    {
        var branches = await branchRepository.GetAllAsync();
        var existBranch = branches.FirstOrDefault(u => u.Location == branch.Location);
        if (existBranch != null)
        {
            if (existBranch.IsDeleted)
                return await UpdateAsync(existBranch.Id, branch.MapTo<BranchUpdateModel>(), true);
            throw new Exception($"This branch is already exist With this location : {branch.Location}");
        }
        var createdBranch = await branchRepository.InsertAsync(branch.MapTo<Branch>());
        return createdBranch.MapTo<BranchViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var branches = await branchRepository.GetAllAsync();
        var existBranch = branches.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This branch is not found With this id {id}");

        await branchRepository.DeleteAsync(id);
        return true;
    }

    public async ValueTask<IEnumerable<BranchViewModel>> GetAllAsync()
    {
        var branches = await branchRepository.GetAllAsync();
        return branches.MapTo<BranchViewModel>();
    }

    public async ValueTask<BranchViewModel> GetByIdAsync(long id)
    {
        var branches = await branchRepository.GetAllAsync();
        var existBranch = branches.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This branch is not found With this id {id}");

        return existBranch.MapTo<BranchViewModel>();
    }

    public async ValueTask<BranchViewModel> UpdateAsync(long id, BranchUpdateModel branch, bool isUsesDeleted)
    {
        var branches = await branchRepository.GetAllAsync();
        var existBranch = new Branch();
        if (isUsesDeleted)
            existBranch = branches.First(u => u.Id == id);
        else
            existBranch = branches.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
                ?? throw new Exception($"This branch is not found With this id {id}");

        var updatedBranch = await branchRepository.UpdateAsync(id, branch.MapTo<Branch>());
        return updatedBranch.MapTo<BranchViewModel>();
    }
}
