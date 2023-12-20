using AlertApi.RouteBuilders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Configuration.GetValue<string>("AccessId");
builder.Configuration.GetValue<string>("SecretKey");
app.RegisterEndpoints();

app.Run();