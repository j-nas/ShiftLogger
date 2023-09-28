namespace ShiftLoggerClient.Models;

public class ShiftWithWorkerDto
{
    public long Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public required WorkerDto Worker { get; set; }
}