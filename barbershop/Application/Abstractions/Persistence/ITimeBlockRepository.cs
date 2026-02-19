using System;
using barbershop.Domain.Entities;

namespace barbershop.Application.Abstractions.Persistence;

public interface ITimeBlockRepository
{
    Task<bool> HasOverlapAsync(Guid employeeId, DateTime startAt, DateTime endAt, CancellationToken ct);
    Task AddAsync(TimeBlock block, CancellationToken ct);
    Task<IReadOnlyList<TimeBlock>> ListByEmployeeAndDayAsync(Guid employeeId, DateTime day, CancellationToken ct);
}
