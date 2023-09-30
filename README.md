# Shift Logger

## Description
REST API for logging shifts, with a console client for interacting with the API.

## Instructions

1.  Clone the repository
2.  In the `ShiftLoggerApi` directory, setup the database connection string in [secrets.json](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=linux)
3.  Initialize the database with `dotnet ef database update`
2.  Start the API with `dotnet run`. Take note of the port number.
3.  In another terminal, in the `ShiftLoggerClient` directory, run `dotnet run https://localhost:<your port number>`. If the URL is not valid, the program will exit.
4.  If the URL is valid, you will be presented with a menu of options. Select an option with arrow keys and confirm with enter.

   