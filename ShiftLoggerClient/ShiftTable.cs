using ShiftLoggerClient.Models;
using Spectre.Console;

namespace ShiftLoggerClient;

internal class ShiftTable
{
    public static Table BuildTable()
    {
        var table = new Table();
        table.AddColumn("Date");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Total Hours");
        return table;
    }

    public static void Render(List<Shift> shifts)
    {
        var table = BuildTable();

        foreach (var shift in shifts)
            table.AddRow(
                shift.Start.ToString("dd/MM/yyyy"),
                shift.Start.ToString("HH:mm"),
                shift.End.ToString("HH:mm"),
                shift.End.Subtract(shift.Start).ToString(@"hh\:mm"));

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[green]Press any key to continue[/]");
        Console.ReadKey();
    }

    public static void RenderOneShift(Shift shift)
    {
        var table = BuildTable();
        table.AddRow(
            shift.Start.ToString("dd/MM/yyyy"),
            shift.Start.ToString("HH:mm"),
            shift.End.ToString("HH:mm"),
            shift.End.Subtract(shift.Start).ToString(@"hh\:mm"));
        AnsiConsole.Write(table);
    }
}