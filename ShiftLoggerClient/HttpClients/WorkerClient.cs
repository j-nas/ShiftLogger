using System.Net;
using System.Text;
using Newtonsoft.Json;
using ShiftLoggerClient.Models;

namespace ShiftLoggerClient.HttpClients;

internal static class WorkerClient
{
    private static readonly Uri? BaseUrl;

    static WorkerClient()
    {
        Uri.TryCreate(Environment.GetCommandLineArgs()[1], UriKind.Absolute, out var baseUrl);
        BaseUrl = baseUrl;
    }

    public static async Task<List<Worker>?> GetWorkers()
    {
        using var client = new HttpClient();
        var response = client.GetAsync($"{BaseUrl}api/worker").Result;
        var content = await response.Content.ReadAsStringAsync();
        var workers = JsonConvert.DeserializeObject<List<Worker>>(content);
        return workers;
    }
    

    public static async Task<Worker> GetWorker(long id)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"{BaseUrl}api/worker/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var worker = JsonConvert.DeserializeObject<Worker>(content);
        return worker ?? throw new Exception("Worker not found");
    }

    public static HttpStatusCode CreateWorker(Worker worker)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(worker);
        var content =
            new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync($"{BaseUrl}api/worker", content).Result;
        return response.StatusCode;
    }

    public static HttpStatusCode UpdateWorker(Worker worker)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(worker);
        var content =
            new StringContent(json, Encoding.UTF8, "application/json");
        var response =
            client.PutAsync($"{BaseUrl}api/worker/{worker.Id}", content).Result;
        return response.StatusCode;
    }

    public static HttpStatusCode DeleteWorker(long id)
    {
        using var client = new HttpClient();
        var response = client.DeleteAsync($"{BaseUrl}api/worker/{id}").Result;
        return response.StatusCode;
    }
}