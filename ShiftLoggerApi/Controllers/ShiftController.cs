using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Data;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly DataContext _context;

    public ShiftController(DataContext context)
    {
        _context = context;
    }

    // GET: api/Shift
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShiftDto>>> GetShifts(
        [FromQuery] long? workerId = null)
    {
        var shifts = await _context.Shifts.ToListAsync();
        if (workerId != null)
            shifts = shifts.Where(s => s.WorkerId == workerId).ToList();
        return Ok(shifts.Select(s => new ShiftDto
        {
            Id = s.Id,
            Start = s.Start,
            End = s.End,
            WorkerId = s.WorkerId
        }));
    }


    // GET: api/Shift/5
    [HttpGet("{id:long}")]
    public async Task<ActionResult<ShiftDto>> GetShift(long id)
    {
        var shift = await _context.Shifts.FindAsync(id);

        if (shift == null) return NotFound();
        shift.Worker = await _context.Workers.FindAsync(shift.WorkerId);

        return Ok(new ShiftDto
        {
            Id = shift.Id,
            Start = shift.Start,
            End = shift.End,
            WorkerId = shift.WorkerId
        });
    }

    // PUT: api/Shift/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShift(long id, ShiftDto shiftDto)
    {
        if (id != shiftDto.Id) return BadRequest();
        var shift = new Shift
        {
            Id = shiftDto.Id,
            Start = shiftDto.Start,
            End = shiftDto.End,
            WorkerId = shiftDto.WorkerId,
            Worker = await _context.Workers.FindAsync(shiftDto.WorkerId)
        };

        _context.Entry(shift).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShiftExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/Shift
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ShiftDto>> PostShift(ShiftDto shiftDto)
    {
        var shift = new Shift
        {
            Start = shiftDto.Start,
            End = shiftDto.End,
            WorkerId = shiftDto.WorkerId,
            Worker = await _context.Workers.FindAsync(shiftDto.WorkerId)
        };
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetShift), new { id = shift.Id },
            shiftDto);
    }

    // DELETE: api/Shift/5
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteShift(long id)
    {
        var shift = await _context.Shifts.FindAsync(id);
        if (shift == null) return NotFound();

        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShiftExists(long id)
    {
        return (_context.Shifts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}