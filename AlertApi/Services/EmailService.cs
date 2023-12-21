using System;
using System.Net;
using System.Net.Mail;
namespace Services
{
    public class EmailService
    {
        public void sendEmail(string from, string to, string subject, string body)
        {
             // COULD NOT CREATE SENDGRID ACCOUNT 
                // I implemented a gmail account instead
                //.... tried at least but due to less secure app settings removed i cant implement this either :D
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