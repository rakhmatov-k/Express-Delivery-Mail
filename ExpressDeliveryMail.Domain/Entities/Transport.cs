using ExpressDeliveryMail.Domain.Commons;
using ExpressDeliveryMail.Domain.Enums;

namespace ExpressDeliveryMail.Domain.Entities;

public class Transport : Auditable
{
    public TransportType Type { get; set; }
    public string Description { get; set; }
    public string Colour { get; set; }
}
