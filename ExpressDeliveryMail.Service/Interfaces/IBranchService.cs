using ExpressDeliveryMail.Domain.Entities;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface IBranchService
{
    ValueTask<Branch> CreatedAsync(Branch branch);
    ValueTask<Branch> UpdateAsync(long id, Branch branch);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Branch> GetByIdAsync(long id);
    ValueTask<IEnumerable<Branch>> GetAllAsync();
}
