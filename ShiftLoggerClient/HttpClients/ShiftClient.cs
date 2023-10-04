using System.Net;
using System.Text;
using Newtonsoft.Json;
using ShiftLoggerClient.Models;

namespace ShiftLoggerClient.HttpClients;

internal static class ShiftClient
{
    private static readonly Uri? BaseUrl;

   static ShiftClient()
   {
       
       Uri.TryCreate(Environment.GetCommandLineArgs()[1], UriKind.Absolute, out var baseUrl);
       BaseUrl = baseUrl;
   }

    public static async Task<List<Shift>?> GetShifts()
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"{BaseUrl}api/shift");
        var content = await response.Content.ReadAsStringAsync();
        var shifts = JsonConvert.DeserializeObject<List<Shift>>(content);
        return shifts;
    }

    public static async Task<List<Shift>> GetShiftByWorker(long workerId)
    {
        using var client = new HttpClient();
        var response =
            await client.GetAsync($"{BaseUrl}api/shift/?workerId={workerId}");
        var content = await response.Content.ReadAsStringAsync();
        var shifts = JsonConvert.DeserializeObject<List<Shift>>(content);
        return shifts;
    }

    public static async Task<Shift> GetShift(long id)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"{BaseUrl}api/shift/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var shift = JsonConvert.DeserializeObject<Shift>(content);
        return shift ?? throw new Exception("Shift not found");
    }

    public static async Task<HttpStatusCode> CreateShift(Shift shift)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(shift);
        var content =
            new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"{BaseUrl}api/shift", content);
        return response.StatusCode;
    }

    public static async Task<HttpStatusCode> UpdateShift(Shift shift)
    {
        using var client = new HttpClient();
        var json = JsonConvert.SerializeObject(shift);
        var content =
            new StringContent(json, Encoding.UTF8, "application/json");
        var response =
            await client.PutAsync($"{BaseUrl}api/shift/{shift.Id}", content);
        return response.StatusCode;
    }

    public static async Task<HttpStatusCode> DeleteShift(long id)
    {
        using var client = new HttpClient();
        var response = await client.DeleteAsync($"{BaseUrl}api/shift/{id}");
        return response.StatusCode;
    }
}