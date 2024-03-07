using ExpressDaliveryMail.Data.Repositories;
using ExpressDeliveryMail.Domain.Entities;
using ExpressDeliveryMail.Service.Extensions;
using ExpressDeliveryMail.Service.Interfaces;

namespace ExpressDeliveryMail.Service.Services;

public class PackageService : IPackageService
{
    private PackageRepository packageRepository;
    private UserService userService;
    private BranchService branchService;
    public PackageService(PackageRepository packageRepository, UserService userService, BranchService branchService)
    {
        this.packageRepository = packageRepository;
        this.userService = userService;
        this.branchService = branchService;
    }

    public async ValueTask<PackageViewModel> CreatedAsync(PackageCreationModel package)
    {
        var existUser = await userService.GetByIdAsync(package.UserId);
        var existStartBranch = await branchService.GetByIdAsync(package.StartBranchId);
        var existEndBranch = await branchService.GetByIdAsync(package.EndBranchId);

        var packages = await packageRepository.GetAllAsync();
        var existPackage = packages.FirstOrDefault(e => e.Status == package.Status && e.StartBranchId == package.StartBranchId);
        if (existPackage is not null)
        {
            if (existPackage.IsDeleted)
                return await UpdateAsync(existPackage.Id, package.MapTo<PackageUpdateModel>(), true);
            throw new Exception($"This package is already exist With this branch id {package.StartBranchId}");
        }

        var createdPackage = await packageRepository.InsertAsync(package.MapTo<Package>());
        return createdPackage.MapTo<PackageViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var packages = await packageRepository.GetAllAsync();
        var existPackage = packages.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
            ?? throw new Exception($"This package is not found With this id {id}");

        await packageRepository.DeleteAsync(id);
        return true;
    }

    public async ValueTask<IEnumerable<PackageViewModel>> GetAllAsync()
    {
        var packages = await packageRepository.GetAllAsync();
        return packages.Where(a => !a.IsDeleted).MapTo<PackageViewModel>();
    }

    public async ValueTask<PackageViewModel> GetByIdAsync(long id)
    {
        var packages = await packageRepository.GetAllAsync();
        var existPackage = packages.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
            ?? throw new Exception($"This package is not found With this id {id}");

        return existPackage.MapTo<PackageViewModel>();
    }

    public async ValueTask<PackageViewModel> UpdateAsync(long id, PackageUpdateModel package, bool isUsesDeleted)
    {
        var existStartBranch = await branchService.GetByIdAsync(package.StartBranchId);
        var existEndBranch = await branchService.GetByIdAsync(package.EndBranchId);
        var existUser = await userService.GetByIdAsync(package.UserId);

        var packages = await packageRepository.GetAllAsync();
        var existPackage = new Package();
        if (isUsesDeleted)
            existPackage = packages.First(a => a.Id == id);
        else
            existPackage = packages.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
                ?? throw new Exception($"This package is not found With this id {id}");

        var updatedPackage = await packageRepository.UpdateAsync(id, package.MapTo<Package>());
        return updatedPackage.MapTo<PackageViewModel>();
    }
}
