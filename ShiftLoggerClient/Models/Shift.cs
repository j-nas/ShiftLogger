using Newtonsoft.Json;

namespace ShiftLoggerClient.Models;

public class Shift
{
    [JsonProperty] public long Id { get; set; }
    [JsonProperty] public DateTime Start { get; set; }
    [JsonProperty] public DateTime End { get; set; }
    [JsonProperty] public long WorkerId { get; set; }
}