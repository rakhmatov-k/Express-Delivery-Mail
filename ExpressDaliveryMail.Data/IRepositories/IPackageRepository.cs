using ExpressDeliveryMail.Domain.Entities;

namespace ExpressDaliveryMail.Data.IRepositories;

public interface IPackageRepository
{
    ValueTask<Package> InsertAsync(Package package);
    ValueTask<Package> UpdateAsync(long id, Package package);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<Package>> GetAllAsync();
}