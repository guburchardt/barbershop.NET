using System;
using barbershop.Domain.Entities;

namespace barbershop.Application.Abstractions.Persistence;

public interface IAppointmentRepository
{
    // There is conflict?
    Task<bool> HasOverlapAsync(Guid employeeId, DateTime startAt, DateTime endAt, CancellationToken ct);
    Task AddAsync(Appointment appointment, CancellationToken ct);
    Task <Appointment?> GetByIdAsync(Guid id, CancellationToken ct);
    Task UpdateAsync (Appointment appointment, CancellationToken ct);
}
