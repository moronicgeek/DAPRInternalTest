using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace Services{
public class TwilioService
{
    public static MessageResource sendSMS(string phoneNumber)
    {
        var accountSid = "AC3da6b1f0e3c57033107e3539de9d56d0";
        var authToken = "ba765d018f5997fc877b2643b892c88e";
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(
          new PhoneNumber(phoneNumber));
        messageOptions.From = new PhoneNumber("+12058597340");
        messageOptions.Body = "Hello from Secure Citizen!";


        var message = MessageResource.Create(messageOptions);
        Console.WriteLine(message.Body);

        return message;
    }
}
}
