using ExpressDeliveryMail.Domain.Entities;
using ExpressDeliveryMail.Domain.Entities.Packages;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface IPackageService
{
    ValueTask<PackageViewModel> CreatedAsync(PackageCreationModel package);
    ValueTask<PackageViewModel> UpdateAsync(long id, PackageUpdateModel package, bool isUsesDeleted);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<PackageViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<PackageViewModel>> GetAllAsync();
}
