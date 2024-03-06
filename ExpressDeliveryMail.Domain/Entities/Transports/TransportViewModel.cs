using ExpressDeliveryMail.Domain.Enums;

namespace ExpressDeliveryMail.Domain.Entities.Transports;

public class TransportViewModel
{
    public long Id { get; set; }
    public TransportType Type { get; set; }
    public string Description { get; set; }
    public string Colour { get; set; }
}
