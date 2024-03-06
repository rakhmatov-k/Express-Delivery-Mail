using ExpressDeliveryMail.Domain.Entities;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface IPackageService
{
    ValueTask<Package> CreatedAsync(Package package);
    ValueTask<Package> UpdateAsync(long id, Package package);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Package> GetByIdAsync(long id);
    ValueTask<IEnumerable<Package>> GetAllAsync();
}
