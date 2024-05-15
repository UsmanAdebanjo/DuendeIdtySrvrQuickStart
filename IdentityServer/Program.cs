using IdentityServer;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddIdentityServer()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients);










var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();