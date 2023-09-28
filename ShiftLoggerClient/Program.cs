using ShiftLoggerClient.Services;

if (args.Length == 0) throw new ArgumentException("Please provide a base url");

var baseUrlResult = Uri.TryCreate(args[0], UriKind.Absolute, out var baseUrl);
if (!baseUrlResult)
    throw new ArgumentException("Please provide a valid base url");

var workerService = new WorkerService(baseUrl.ToString());
var workers = await workerService.GetWorkers();

if (workers != null)
{
    var worker = workers.Find(x => x.Id == 3);
    Console.WriteLine($"Worker with id 2 is {worker.Name}");
}