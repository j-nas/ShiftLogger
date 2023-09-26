using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Data;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkerController : ControllerBase
{
    private readonly DataContext _context;

    public WorkerController(DataContext context)
    {
        _context = context;
    }

    // GET: api/Worker
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkerDto>>> GetWorkers()
    {
        var workers = await _context.Workers.ToListAsync();
        return Ok(workers.Select(w => new WorkerDto
        {
            Id = w.Id,
            Name = w.Name
        }));
    }

    // GET: api/Worker/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkerDto>> GetWorker(long id)
    {
        var worker = await _context.Workers.FindAsync(id);

        if (worker == null) return NotFound();

        return Ok(new WorkerDto
        {
            Id = worker.Id,
            Name = worker.Name
        });
    }

    // PUT: api/Worker/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorker(long id, WorkerDto workerDto)
    {
        if (id != workerDto.Id) return BadRequest();
        
        var worker = await _context.Workers.FindAsync(id);
        if (worker == null) return NotFound();
        
        worker.Name = workerDto.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!WorkerExists(id))
        {
            if (!WorkerExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/Worker
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Worker>> PostWorker(WorkerDto worker)
    {
        _context.Workers.Add(new Worker
        {
            Name = worker.Name
        });
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetWorker", new { id = worker.Id }, worker);
    }

    // DELETE: api/Worker/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorker(long id)
    {
        var worker = await _context.Workers.FindAsync(id);
        if (worker == null) return NotFound();

        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WorkerExists(long id)
    {
        return (_context.Workers?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}