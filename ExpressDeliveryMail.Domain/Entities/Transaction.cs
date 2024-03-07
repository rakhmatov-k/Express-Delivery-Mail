using ExpressDeliveryMail.Domain.Commons;
using ExpressDeliveryMail.Domain.Entities.Expresses;

namespace ExpressDeliveryMail.Domain.Entities;

public class Transaction : Auditable
{
    public long ExpressId { get; set; }
    public Express Express { get; set; }
    public long PackageId { get; set; }
    public Package Package { get; set; }
}
