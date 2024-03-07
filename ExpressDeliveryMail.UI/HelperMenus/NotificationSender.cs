using System.Net;
using System.Net.Mail;

namespace ExpressDeliveryMail.UI.HelperMenus;

public class NotificationSender
{
    private static void SendEmail(string receiver, string subject, string content)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("husnidabonu224@gmail.com");
        mailMessage.To.Add(receiver);
        mailMessage.Subject = subject;
        mailMessage.Body = content;

        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = "smtp.gmail.com";
        smtpClient.Port = 587;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential("husnidabonu224@gmail.com", "zned lxdt soxl ygau");
        smtpClient.EnableSsl = true;

        try
        {
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email Sent Successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}