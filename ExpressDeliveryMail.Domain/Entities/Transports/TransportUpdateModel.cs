using ExpressDeliveryMail.Domain.Enums;

namespace ExpressDeliveryMail.Domain.Entities.Transports;

public class TransportUpdateModel
{
    public TransportType Type { get; set; }
    public string Description { get; set; }
    public string Colour { get; set; }
}
