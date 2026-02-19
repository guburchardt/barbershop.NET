using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Infrastructure.Persistence.InMemory;

public class InMemoryTimeBlockRepository : ITimeBlockRepository
{
    private static readonly List<TimeBlock> _blocks = new();

    public Task AddAsync(TimeBlock block, CancellationToken ct)
    {
        _blocks.Add(block);
        return Task.CompletedTask;
    }

    public Task<bool> HasOverlapAsync(Guid employeeId, DateTime startAt, DateTime endAt, CancellationToken ct)
    {
        var overlaps = _blocks.Any(b =>
            (b.EmployeeId == null || b.EmployeeId == employeeId) &&
            startAt < b.EndAt &&
            endAt > b.StartAt
        );

        return Task.FromResult(overlaps);
    }

    public Task<IReadOnlyList<TimeBlock>> ListByEmployeeAndDayAsync(Guid employeeId, DateTime day, CancellationToken ct)
    {
        var start = day.Date;
        var end = start.AddDays(1);

        var result = _blocks
            .Where(b =>
                (b.EmployeeId == null || b.EmployeeId == employeeId) &&
                b.StartAt < end &&
                b.EndAt > start
            )
            .OrderBy(b => b.StartAt)
            .ToList();

        return Task.FromResult((IReadOnlyList<TimeBlock>)result);
    }

    public Task<TimeBlock?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var block = _blocks.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(block);
    }

    public Task RemoveAsync(TimeBlock block, CancellationToken ct)
    {
        _blocks.Remove(block);
        return Task.CompletedTask;
    }
}
