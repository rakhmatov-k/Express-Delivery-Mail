﻿using ExpressDeliveryMail.Domain.Commons;
using ExpressDeliveryMail.Domain.Enums;

namespace ExpressDeliveryMail.Domain.Entities;

public class User : Auditable
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public decimal Balance { get; set; }
    public string Password { get; set; }
}
