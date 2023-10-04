using System.Net;
using ShiftLoggerClient.HttpClients;
using ShiftLoggerClient.Models;
using Spectre.Console;

namespace ShiftLoggerClient.Services;

internal static class WorkerService
{
    internal static void AddWorker()
    {
        var worker = new Worker
        {
            Name = AnsiConsole.Ask<string>("What is the name of the worker?")
        };
        var result = WorkerClient.CreateWorker(worker);
        AnsiConsole.MarkupLine(result == HttpStatusCode.Created
            ? "[green]Worker created successfully[/]"
            : "[red]Worker creation failed[/]");
    }

    internal static Worker SelectWorker()
    {
        var workers = WorkerClient.GetWorkers();
        var result = workers.Result;
        if (result is null)
        {
            AnsiConsole.MarkupLine(
                "[red]No workers found. Please add one in the main menu[/]");
            return new Worker();
        }

        var worker = AnsiConsole.Prompt(
            new SelectionPrompt<Worker>()
                .Title("Select a worker")
                .AddChoices(result)
                .UseConverter(worker => worker.Name!)
        );
        return worker;
    }

    internal static void UpdateWorker(Worker worker)
    {
        worker.Name =
            AnsiConsole.Ask<string>("What is the new name of the worker?");
        var result = WorkerClient.UpdateWorker(worker);
        AnsiConsole.MarkupLine(result == HttpStatusCode.NoContent
            ? "[green]Worker updated successfully[/]"
            : "[red]Worker update failed[/]");
    }
    internal static void DeleteWorker(Worker worker)
    {
        var confirm = AnsiConsole.Confirm(
            "Are you sure you want to delete this worker? This cannot be undone");
        if (!confirm) return;
        var result = WorkerClient.DeleteWorker(worker.Id);
        AnsiConsole.MarkupLine(result == HttpStatusCode.NoContent
            ? "[green]Worker deleted successfully[/]"
            : "[red]Worker deletion failed[/]");
    }
}