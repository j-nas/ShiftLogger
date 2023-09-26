namespace ShiftLoggerApi.Models;

public class ShiftDto
{
    public long Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public long WorkerId { get; set; }
}