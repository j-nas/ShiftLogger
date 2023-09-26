namespace ShiftLoggerApi.Models;

public class Shift
{
    public long Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public long WorkerId { get; set; }
    public Worker? Worker { get; set; } = null!;
}