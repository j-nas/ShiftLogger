namespace ShiftLoggerApi.Models;

public class Worker
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public List<Shift>? Shifts { get; set; }
}