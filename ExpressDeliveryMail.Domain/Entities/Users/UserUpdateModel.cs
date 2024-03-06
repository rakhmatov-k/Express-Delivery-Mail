using ExpressDeliveryMail.Domain.Enums;

namespace ExpressDeliveryMail.Domain.Entities.Users;

public class UserUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public string Password { get; set; }
}
