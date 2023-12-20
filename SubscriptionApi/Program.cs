using System.Text.Json.Serialization;
using Common.Models.Requests;
// using Common.Models.Requests.Publish;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SubscriptionApi;

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
    //return a 200 response to the publisher

    return new JsonResult(new PublishAlertResponse { Status = "Success" }) { StatusCode = 200 };
});



await app.RunAsync();
