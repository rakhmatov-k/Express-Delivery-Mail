using System.Security.Cryptography;
using System.Text;

namespace ExpressDeliveryMail.UI.HelperMenus;

public class PasswordActions
{
    public static string Hashing(string password)
    {
        using (SHA256 hash = SHA256.Create())
        {
            byte[] hashedBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
    public static bool VerifyPassword(string actualHashedPassword, string enteredPassword)
    {
        string enteredHashedPassword = Hashing(enteredPassword);
        return actualHashedPassword == enteredHashedPassword;
    }
}
