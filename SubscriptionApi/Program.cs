using System.Text.Json.Serialization;
using Common.Models.Requests;
// using Common.Models.Requests.Publish;
using Dapr;
using Dapr.Client;

// Create a DaprClient instance
var daprClient = new DaprClientBuilder().Build();
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

// This endpoint should subscribe to a topic and will then write to the console information about the data received
// Subscription can be via redis or rabbitmq
app.MapPost("/alert", async (PublishAlertRequest pubRequest) =>
{


    // Subscribe to a topic and receive responses via redis or rabbitmq
    await daprClient.PublishEventAsync("pubsub", "subscriberapi-topic", pubRequest);
    

    Console.WriteLine("Subscriber received : " + pubRequest.AlertType + "\nSent at time : " + pubRequest.PublishRequestTime);
});

await app.RunAsync();
