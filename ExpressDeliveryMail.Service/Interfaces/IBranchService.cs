using ExpressDeliveryMail.Domain.Entities.Branches;

namespace ExpressDeliveryMail.Service.Interfaces;

public interface IBranchService
{
    ValueTask<BranchViewModel> CreatedAsync(BranchCreationModel branch);
    ValueTask<BranchViewModel> UpdateAsync(long id, BranchUpdateModel branch, bool isUsesDeleted );
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<BranchViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<BranchViewModel>> GetAllAsync();
}
