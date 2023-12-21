using Common.Interfaces.Requests;
using Common.Models.Requests;
// using Common.Models.Requests.Publish;
using Common.Models.Responses;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services;

namespace AlertApi.RouteBuilders
{
    public static class EndpointFactory
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            app.MapPost("/sendSms", async ([FromBody] string phoneNumber) =>
            {
                // Invoke a binding to an sms service provider (You will need to create your own twilion account) 
                // No body as free tier of twilio offers no message capability
                using var client = new DaprClientBuilder().Build();
                //Validate phoneNumber starts with a +
                if (phoneNumber[0] != '+' && phoneNumber.Length != 12)
                {
                    app.Logger.LogInformation("Invalid phone number");
                    return new AlertResponse { Response = "Invalid phone number" };
                }

                var twilio = new SMSService(app.Configuration);
                var message = twilio.sendSMS(phoneNumber);
                Console.WriteLine(message.Body);

                app.Logger.LogInformation("SMS sent succesfully");
                return new AlertResponse { Response = "SMS sent succesfully" };
            });

            app.MapPost("/sendEmail", async ([FromBody] string emailAddress) =>
            {
                // Invoke a binding to an email service provider (you will need to create your own sendgrid account)


                // COULD NOT CREATE SENDGRID ACCOUNT 


                using var client = new DaprClientBuilder().Build();
                //validate the email address
                if (!emailAddress.Contains("@"))
                {
                    app.Logger.LogInformation("Invalid email address");
                    return new AlertResponse { Response = "Invalid email address" };
                }


                //send email here
                //  var emailService = new EmailService();
                //  emailService.sendEmail("info@securecitizen.com", emailAddress, "Hello from Secure Citizen!", "Hello from Secure Citizen!");


                app.Logger.LogInformation("Email sent succesfully");
                return new AlertResponse { Response = "Email sent succesfully" };
            });

            app.MapPost("/sendAlert", async (AlertRequest alertRequest) =>
            {
                // Publish multiple events via rabbitmq (could also use redis but only in self-hosted kubernetes mode) 
                using var client = new DaprClientBuilder().Build();

                for (int i = 0; i < alertRequest.AlertTypes.Count(); i++)
                {
                    app.Logger.LogInformation("Publishing alert: " + alertRequest.AlertTypes[i] + "\n      For client: " + alertRequest.ClientName);
                    DateTime now = DateTime.Now;

                    PublishAlertRequest pubRequest = new PublishAlertRequest()
                    {
                        AlertType = alertRequest.AlertTypes[i],
                        PublishRequestTime = now
                    };

                    app.Logger.LogInformation("Published data at: " + pubRequest.PublishRequestTime);

                    await client.PublishEventAsync("pubsub", "alerts", pubRequest);

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }

                return new AlertResponse { Response = "Alerts sent succesfully" };
            });
        }
    }
}
