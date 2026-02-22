using System;
using barbershop.Domain.Entities;

namespace barbershop.Application.Abstractions.Persistence;

public interface ITimeBlockRepository
{
    Task<bool> HasOverlapAsync(Guid? employeeId, DateTime startAt, DateTime endAt, CancellationToken ct, Guid? excludeId = null);
    Task AddAsync(TimeBlock block, CancellationToken ct);
    Task<IReadOnlyList<TimeBlock>> ListByEmployeeAndDayAsync(Guid? employeeId, DateTime day, CancellationToken ct);
    Task<TimeBlock?> GetByIdAsync(Guid id, CancellationToken ct);
    Task RemoveAsync(TimeBlock block, CancellationToken ct);
    Task UpdateAsync(TimeBlock block, CancellationToken ct);
}
