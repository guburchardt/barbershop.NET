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

    public Task<bool> HasOverlapAsync(Guid? employeeId, DateTime startAt, DateTime endAt, CancellationToken ct, Guid? excludeId = null)
    {
        var overlaps = _blocks.Any(b =>
            b.Id != excludeId &&
            (b.EmployeeId == null || employeeId == null || b.EmployeeId == employeeId) &&
            startAt < b.EndAt &&
            endAt > b.StartAt
        );

        return Task.FromResult(overlaps);
    }


    public Task<IReadOnlyList<TimeBlock>> ListByEmployeeAndDayAsync(Guid? employeeId, DateTime day, CancellationToken ct)
    {
        var start = day.Date;
        var end = start.AddDays(1);

        var result = _blocks
            .Where(b =>
                // Se vir employeeId, filtra por ele ou pelos blocos globais (null).
                // Se NÃO vir employeeId, traz tudo o que bater com a data.
                (employeeId == null || b.EmployeeId == null || b.EmployeeId == employeeId) &&
                b.StartAt < end && b.EndAt > start
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

    public Task UpdateAsync(TimeBlock block, CancellationToken ct)
    {
        // Como é uma lista em memória e estamos alterando a própria referência do objeto, 
        // no InMemory não precisaríamos fazer nada. Mas para seguir o padrão:
        var index = _blocks.FindIndex(b => b.Id == block.Id);
        if (index != -1)
        {
            _blocks[index] = block;
        }
        return Task.CompletedTask;
    }
}
