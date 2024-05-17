// See https://aka.ms/new-console-template for more information
using IdentityModel.Client;
using System.Text.Json;

//Console.WriteLine("Hello, World!");

// discover endpoints from metadata
var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7275");
if (disco.IsError)
{
    //prints this if error exist which means disco.IsError=true
    Console.WriteLine("Oh I got an error --->  {0}",disco.Error);
    
    return;
}

// request token
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,

    ClientId = "client",
    ClientSecret = "secret",
    Scope = "api1"
});

if (tokenResponse.IsError)
{
    Console.WriteLine("There is error getting access token---> {0}",tokenResponse.Error);
    return;
}

Console.WriteLine("Here is the access token ----->  {0}",tokenResponse.AccessToken);
Thread.Sleep(20000);


// call api
var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:7239/api/identity");
if (!response.IsSuccessStatusCode)
{

    Console.WriteLine(response.StatusCode);
    Console.WriteLine("I am not authorised to get info from the api ");
}
else
{
    var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
    Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
}

Thread.Sleep(80000);

