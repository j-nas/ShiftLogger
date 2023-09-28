using System.Net;
using System.Text;
using Newtonsoft.Json;
using ShiftLoggerClient.Models;

namespace ShiftLoggerClient.Services;

internal class WorkerService
{
    private readonly string _baseUrl;

    public WorkerService(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    public async Task<List<Worker>?> GetWorkers()
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"{_baseUrl}api/worker");
        var content = await response.Content.ReadAsStringAsync();
        var workers = JsonConvert.DeserializeObject<List<Worker>>(content);
        return workers;
    }

    public async Task<Worker> GetWorker(long id)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"{_baseUrl}api/worker/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var worker = JsonConvert.DeserializeObject<Worker>(content);
        return worker ?? throw new Exception("Worker not found");
    }

    public async Task<HttpStatusCode> CreateWorker(Worker worker)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(worker);
        var content =
            new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"{_baseUrl}api/worker", content);
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> UpdateWorker(Worker worker)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(worker);
        var content =
            new StringContent(json, Encoding.UTF8, "application/json");
        var response =
            await client.PutAsync($"{_baseUrl}api/worker/{worker.Id}", content);
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> DeleteWorker(long id)
    {
        using var client = new HttpClient();
        var response = await client.DeleteAsync($"{_baseUrl}api/worker/{id}");
        return response.StatusCode;
    }
}