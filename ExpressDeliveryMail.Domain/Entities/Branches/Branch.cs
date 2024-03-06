using ExpressDeliveryMail.Domain.Commons;

namespace ExpressDeliveryMail.Domain.Entities.Branches;

public class Branch : Auditable
{
    public string Name { get; set; }
    public string Location { get; set; }
    public float Rating { get; set; }
}
