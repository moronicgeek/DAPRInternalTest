using Dapr.Client;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Common.Models.Requests;

using var client = new DaprClientBuilder().Build();

List<string> alerts = new() { "Alert1", "Alert2", "Alert3" };

var alertrequest = new AlertRequest
{
    AlertTypes = alerts,
    ClientName = "Generic Client"
};




// Invoke each method of the AlertApi here
//var smsData = new Dictionary<string, string> { { "phoneNumber", "1234567890" } };
await client.InvokeMethodAsync("alertapi", "/sendSms", "+27665026084");

// Invoke the /sendEmail method
var emailData = new Dictionary<string, string> { { "emailAddress", "test@example.com" } };
await client.InvokeMethodAsync("alertapi", "/sendEmail", "patel.muhammed@gmail.com");

// Invoke the /sendAlert method
await client.InvokeMethodAsync<AlertRequest>("alertapi", "/sendAlert", alertrequest);


Console.Read();

