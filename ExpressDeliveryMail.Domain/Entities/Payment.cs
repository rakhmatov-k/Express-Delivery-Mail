﻿using ExpressDeliveryMail.Domain.Commons;
using ExpressDeliveryMail.Domain.Entities.Users;
using ExpressDeliveryMail.Domain.Enums;

namespace ExpressDeliveryMail.Domain.Entities;

public class Payment : Auditable
{
    public long PackageId { get; set; }
    public Package Package { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
}
