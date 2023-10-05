using System.Net;
using ShiftLoggerClient.HttpClients;
using ShiftLoggerClient.Models;
using Spectre.Console;

namespace ShiftLoggerClient.Services;

internal static class ShiftServices
{
    internal static void LogShift(long workerId)
    {
        var isPromptRunning = true;
        while (isPromptRunning)
        {
            AnsiConsole.MarkupLine("[green]Enter start time[/]");
            var startTime = DatePicker.GetDateTime();
            AnsiConsole.MarkupLine("[green]Enter end time[/]");
            var endTime = DatePicker.GetDateTime();
            if (endTime < startTime)
            {
                AnsiConsole.MarkupLine(
                    "[red]End time cannot be before start time[/]");
                AnsiConsole.MarkupLine("[red]Press any key to continue[/]");
                Console.ReadKey();
                continue;
            }

            if (AnsiConsole.Confirm("Is this correct?"))
            {
                isPromptRunning = false;
                continue;
            }


            var shift = new Shift
            {
                Start = startTime,
                End = endTime,
                WorkerId = workerId
            };
            var result = ShiftClient.CreateShift(shift);
            AnsiConsole.MarkupLine(result.Result == HttpStatusCode.Created
                ? "[green]Shift created successfully[/]"
                : "[red]Shift creation failed[/]");
            isPromptRunning = false;
        }
    }

    public static void ViewShifts(long workerId)

    {
        var shifts = ShiftClient.GetShiftByWorker(workerId).Result;
        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine(
                "[red]No shifts found. Please add one in the main menu[/]");
            AnsiConsole.MarkupLine("[red]Press any key to continue[/]");
            Console.ReadKey();
            return;
        }

        ShiftTable.Render(shifts);
    }

    public static Shift SelectShift(long workerId)

    {
        var shifts = ShiftClient.GetShiftByWorker(workerId).Result;

        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine(
                "[red]No shifts found. Please add one in the main menu[/]");
            return new Shift();
        }

        var shift = AnsiConsole.Prompt(
            new SelectionPrompt<Shift>()
                .Title("Select a shift")
                .AddChoices(shifts)
                .UseConverter(shift => shift.Start.ToString("dd/MM/yyyy")
                ));
        return shift;
    }

    public static void UpdateShift(Shift shift)
    {
        shift.Start = AnsiConsole.Confirm("Do you want to change the start time?")
            ? DatePicker.GetDateTime()
            : shift.Start;
        shift.End = AnsiConsole.Confirm("Do you want to change the end time?")
            ? DatePicker.GetDateTime()
            : shift.End;
        var result = ShiftClient.UpdateShift(shift).Result;
        AnsiConsole.MarkupLine(result == HttpStatusCode.NoContent
            ? "[green]Shift updated successfully[/]"
            : "[red]Shift update failed[/]");
    }

    public static void DeleteShift(Shift shift)
    {
        var confirm = AnsiConsole.Confirm(
            "Are you sure you want to delete this shift? This cannot be undone");
        if (!confirm) return;
        var result = ShiftClient.DeleteShift(shift.Id).Result;
        AnsiConsole.MarkupLine(result == HttpStatusCode.NoContent
            ? "[green]Shift deleted successfully[/]"
            : "[red]Shift deletion failed[/]");
    }
}