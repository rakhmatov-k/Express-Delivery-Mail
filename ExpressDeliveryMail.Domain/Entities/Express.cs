﻿using ExpressDeliveryMail.Domain.Commons;
using ExpressDeliveryMail.Domain.Entities.Branches;
using ExpressDeliveryMail.Domain.Entities.Transports;

namespace ExpressDeliveryMail.Domain.Entities;

public class Express : Auditable
{
    public decimal Distance { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public long TransportId { get; set; }
    public Transport Transport { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
}
