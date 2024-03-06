using ExpressDeliveryMail.Domain.Commons;
using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Domain.Enums;

namespace ExpressDeliveryMail.Domain.Entities;

public class Package : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long StartBranchId { get; set; }
    public Branch StartBranch { get; set; }
    public long EndBranchId { get; set; }
    public Branch EndBranch { get; set; }
    public PackageStatus Status { get; set; }
    public PackageCategory Category { get; set; }
    public float Weight { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverPhone { get; set; }
}
