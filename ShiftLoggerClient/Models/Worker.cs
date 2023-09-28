using Newtonsoft.Json;

namespace ShiftLoggerClient.Models;

public class Worker
{
    [JsonProperty("id")] public long Id { get; set; }
    [JsonProperty("name")] public string? Name { get; set; }
}