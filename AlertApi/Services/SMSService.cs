using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

public class SMSService
{
  private readonly IConfiguration config;

  public SMSService(IConfiguration configuration)
  {
    config = configuration;
  }

  public  MessageResource sendSMS(string phoneNumber)
  {
    var accountSid = config["Twilio:accountSID"];
    var authToken = config["Twilio:authToken"];
    var fromPhoneNumber = config["Twilio:fromNumber"];

    TwilioClient.Init(accountSid, authToken);

    var messageOptions = new CreateMessageOptions(new Twilio.Types.PhoneNumber(phoneNumber));
    messageOptions.From = new Twilio.Types.PhoneNumber(fromPhoneNumber);
    messageOptions.Body = "Hello from Secure Citizen!";

    var message = MessageResource.Create(messageOptions);
    Console.WriteLine(message.Body);

    return message;
  }
}
