using ShiftLoggerClient.Enums;
using ShiftLoggerClient.HttpClients;
using ShiftLoggerClient.Models;
using ShiftLoggerClient.Services;
using Spectre.Console;

namespace ShiftLoggerClient;

internal static class UserInterface
{
    private static void Title()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("ShiftLogger")
            .LeftJustified()
            .Color(Color.Green));
    }

    internal static void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            Title();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                    .Title("What do you want to do?")
                    .AddChoices(Enum.GetValues(typeof(MainMenuOptions))
                        .Cast<MainMenuOptions>()));

            switch (option)
            {
                case MainMenuOptions.SelectWorker:
                    var worker = WorkerService.SelectWorker();
                    WorkerMenu(worker);

                    break;
                case MainMenuOptions.AddWorker:

                    WorkerService.AddWorker();
                    break;
                case MainMenuOptions.Exit:
                default:
                    isAppRunning = false;
                    break;
            }
        }
    }

    private static void WorkerMenu(Worker worker)
    {
        var isWorkerMenuRunning = true;
        while (isWorkerMenuRunning)
        {
            Title();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<WorkerMenuOptions>()
                    .Title($"Choose an action for {worker.Name}:")
                    .AddChoices(Enum.GetValues(typeof(WorkerMenuOptions))
                        .Cast<WorkerMenuOptions>()));

            switch (option)
            {
                case WorkerMenuOptions.SubmitShift:
                    ShiftServices.LogShift(worker.Id);
                    break;
                case WorkerMenuOptions.ViewShifts:
                    ShiftServices.ViewShifts(worker.Id);
                    break;
                case WorkerMenuOptions.ModifyShifts:
                    var shift = ShiftServices.SelectShift(worker.Id);
                    ShiftMenu(shift);
                    break;
                case WorkerMenuOptions.RenameWorker:
                    WorkerService.UpdateWorker(worker);
                    break;
                case WorkerMenuOptions.DeleteWorker:
                    WorkerService.DeleteWorker(worker);
                    break;
                case WorkerMenuOptions.GoBack:
                default:
                    isWorkerMenuRunning = false;
                    break;
            }
        }
    }

    private static void ShiftMenu(Shift shift)
    {
        var isShiftMenuRunning = true;
        while (isShiftMenuRunning)
        {
            Title();
            ShiftTable.RenderOneShift(shift);
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<ShiftMenuOptions>()
                    .Title($"Choose an action for the shift on {shift.Start:d}:")
                    .AddChoices(Enum.GetValues(typeof(ShiftMenuOptions))
                        .Cast<ShiftMenuOptions>()));
            switch (option)
            {
                case ShiftMenuOptions.UpdateShift:
                    ShiftServices.UpdateShift(shift);
                    break;
                case ShiftMenuOptions.DeleteShift:
                    ShiftServices.DeleteShift(shift);
                    isShiftMenuRunning = false;
                    break;
                case ShiftMenuOptions.GoBack:
                default:
                    isShiftMenuRunning = false;
                    break;
            }
        }
    }
}