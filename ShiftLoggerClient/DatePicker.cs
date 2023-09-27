using ShiftLoggerClient.Enums;
using Spectre.Console;

namespace ShiftLoggerClient;

internal static class DatePicker
{
    public static DateTime GetDateTime()
    {
        var year = GetYear();
        var month = GetMonth();
        return new DateTime(
            year,
            month,
            GetNumber(DateTime.DaysInMonth(year, month), "day", 1),
            GetNumber(24, "hour"),
            GetNumber(60, "minute"),
            0);
    }


    private static int GetYear()
    {
        var yearList = new List<int>
        {
            DateTime.Now.Year,
            DateTime.Now.Year - 1
        };

        var year = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select year")
                .AddChoices(yearList));
        return year;
    }

    private static int GetMonth()
    {
        return (int)AnsiConsole.Prompt(
            new SelectionPrompt<Months>()
                .Title("Select month")
                .AddChoices(Enum.GetValues(typeof(Months)).Cast<Months>()));
    }
    private static int GetNumber(int range, string prompt, int index = 0)
    {
        var hour = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title($"Select {prompt}")
                .AddChoices(Enumerable.Range(index, range)));
        return hour;
    }
}