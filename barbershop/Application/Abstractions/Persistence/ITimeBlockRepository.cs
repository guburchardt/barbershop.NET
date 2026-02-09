using System;

namespace barbershop.Application.Abstractions.Persistence;

public interface ITimeBlockRepository
{
    Task<bool> HasOverlapAsync(Guid employeeId, DateTime startAt, DateTime endAt, CancellationToken ct);
}
