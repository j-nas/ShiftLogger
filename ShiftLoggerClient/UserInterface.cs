using ShiftLoggerClient.Enums;
using ShiftLoggerClient.HttpClients;
using ShiftLoggerClient.Models;
using ShiftLoggerClient.Services;
using Spectre.Console;
using static ShiftLoggerClient.Enums.MainMenuOptions;
using static ShiftLoggerClient.Enums.WorkerMenuOptions;

namespace ShiftLoggerClient;

internal static class UserInterface
{
    internal static void Title()
    {
        Console.Clear();
        AnsiConsole.Write(new FigletText("ShiftLogger")
            .LeftJustified()
            .Color(Color.Green));
    }

    internal static async void MainMenu()
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
                case SelectWorker:
                    var worker = WorkerService.SelectWorker();
                    WorkerMenu(worker);
                    
                    break;
                case AddWorker:
                    
                    WorkerService.AddWorker();
                    break;
                case Exit:
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
                case SubmitShift:
                    ShiftServices.LogShift(worker.Id);
                    break;
                case ViewShifts:
                    ShiftServices.ViewShifts(worker.Id);
                    break;
                case ModifyShifts:
                    ShiftServices.SelectShift(worker.Id);
                    break;
                case RenameWorker:
                    WorkerService.UpdateWorker(worker);
                    break;
                case DeleteWorker:
                    WorkerService.DeleteWorker(worker);
                    break;
                case GoBack:
                default:
                    isWorkerMenuRunning = false;
                    break;
            }
        }
    }
}