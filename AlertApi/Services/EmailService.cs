using System;
using System.Net;
using System.Net.Mail;
namespace Services
{
    public class EmailService
    {
        public void SendEmail(string from, string to, string subject, string body)
        {
            try
            {
                using (var message = new MailMessage(from, to))
                {
                    message.Subject = subject;
                    message.Body = body;

                    using (var client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("your-email@gmail.com", "your-password");

                        client.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sending the email: " + ex.Message);
            }
        }
    }
}