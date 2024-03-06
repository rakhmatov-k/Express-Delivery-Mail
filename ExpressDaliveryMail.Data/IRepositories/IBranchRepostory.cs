using ExpressDeliveryMail.Domain.Entities.Branches;

namespace ExpressDaliveryMail.Data.IRepositories;

public interface IBranchRepostory
{
    ValueTask<Branch> InsertAsync(Branch branch);
    ValueTask<Branch> UpdateAsync(long id, Branch branch);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<Branch>> GetAllAsync();
}
