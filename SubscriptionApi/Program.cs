
using Common.Models.Requests;
// using Common.Models.Requests.Publish;
using Dapr;
using Dapr.Client;


// Create a DaprClient instance
var daprClient = new DaprClientBuilder().Build();
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapSubscribeHandler();

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

// This endpoint should subscribe to a topic and will then write to the console information about the data received
// Subscription can be via redis 

app.MapPost("/alert", [Topic("pubsub", "alerts")] async (PublishAlertRequest pubRequest) =>
{
    //receive messages from topic "subscriberapi-topic" usinf dapr client pub sub component
    Console.WriteLine("Subscriber received : " + pubRequest.AlertType + "\nSent at time : " + pubRequest.PublishRequestTime);

    return new PublishAlertRequest { AlertType = pubRequest.AlertType, PublishRequestTime = pubRequest.PublishRequestTime };
});



await app.RunAsync();
