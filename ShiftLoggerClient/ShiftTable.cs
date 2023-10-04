using ShiftLoggerClient.Models;
using Spectre.Console;

namespace ShiftLoggerClient;

internal class ShiftTable
{
    public static void Render(List<Shift> shifts)
    {
        UserInterface.Title();
        var table = new Table();
        table.AddColumn("Date");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Total Hours");
    
        foreach (var shift in shifts)
        {
            table.AddRow(
                shift.Start.ToString("dd/MM/yyyy"),
                shift.Start.ToString("HH:mm"),
                shift.End.ToString("HH:mm"),
                shift.End.Subtract(shift.Start).ToString(@"hh\:mm"));
        }
        
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[green]Press any key to continue[/]");
        Console.ReadKey();
    }
}