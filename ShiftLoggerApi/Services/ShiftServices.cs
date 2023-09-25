using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Data;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Services;

internal class ShiftServices : IShiftServices
{
    private readonly DataContext _context;
    
    internal ShiftServices(DataContext context)
    {
        _context = context;
    }
    public Task<List<ShiftWithWorkerDto>> GetAll()
    {
        var shiftList = _context.Shifts
            .Include(s => s.Worker)
            .Select(s => new ShiftWithWorkerDto
            {
                Id = s.Id,
                Start = s.Start,
                End = s.End,
                Worker = new WorkerDto
                {
                    Id = s.Worker!.Id,
                    Name = s.Worker.Name
                }
            })
            .ToListAsync();

        return shiftList;
    }

    public Task<ShiftWithWorkerDto?> GetById(long id)
    {
        return _context.Shifts
            .Include(s => s.Worker)
            .Select(s => new ShiftWithWorkerDto
            {
                Id = s.Id,
                Start = s.Start,
                End = s.End,
                Worker = new WorkerDto
                {
                    Id = s.Worker!.Id,
                    Name = s.Worker.Name
                }
            })
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<ShiftWithWorkerDto?> Create(ShiftDto shift)
    {
        var newShift = new Shift
        {
            Start = shift.Start,
            End = shift.End,
            WorkerId = shift.WorkerId
        };
        _context.Shifts.Add(newShift);
        await _context.SaveChangesAsync();
        return await GetById(newShift.Id);
    }

    public async Task<ShiftWithWorkerDto?> Update(long id, ShiftDto shift)
    {
        var updatedShift = new Shift
        {
            Id = id,
            Start = shift.Start,
            End = shift.End,
            WorkerId = shift.WorkerId
        };
        _context.Shifts.Update(updatedShift);
        await _context.SaveChangesAsync();
        return await GetById(id);
    }

    public Task<bool> Delete(long id)
    {
       var 
    }
}

public interface IShiftServices
{
    Task<List<ShiftWithWorkerDto>> GetAll();
    Task<ShiftWithWorkerDto?> GetById(long id);
    Task<ShiftWithWorkerDto?> Create(ShiftDto shift);
    Task<ShiftWithWorkerDto?> Update(long id, ShiftDto shift);
    Task<bool> Delete(long id);
}