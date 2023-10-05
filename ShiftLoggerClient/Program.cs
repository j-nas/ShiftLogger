using ShiftLoggerClient;
using ShiftLoggerClient.Services;

if (args.Length == 0)
    throw new ArgumentException(@"Please provide the url for the api
Example: dotnet run https://localhost:7056");

var baseUrlResult = Uri.TryCreate(Environment.GetCommandLineArgs()[1],
    UriKind.Absolute, out _);
if (!baseUrlResult)
    throw new ArgumentException("Please provide a valid url");
try
{
    using var client = new HttpClient();
    var response = client.GetAsync($"{args[0]}api/ping").Result;
    if (!response.IsSuccessStatusCode)
        throw new HttpRequestException();
}
catch (HttpRequestException)
{
    throw new HttpRequestException(
        "Please provide a valid url, or make sure the api is running");
}

UserInterface.MainMenu();