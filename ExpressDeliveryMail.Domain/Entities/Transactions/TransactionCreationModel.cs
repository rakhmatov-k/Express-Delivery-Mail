using ExpressDeliveryMail.Domain.Entities.Expresses;

namespace ExpressDeliveryMail.Domain.Entities.Transactions;

public class TransactionCreationModel
{
    public long ExpressId { get; set; }
    public Express Express { get; set; }
    public long PackageId { get; set; }
    public Package Package { get; set; }
}
