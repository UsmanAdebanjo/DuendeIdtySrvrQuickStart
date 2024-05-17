using IdentityServer;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddIdentityServer()
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients);










var app = builder.Build();

app.MapGet("/", () => "Hello I am IdentityServer Project!");
app.UseIdentityServer();
app.Run();