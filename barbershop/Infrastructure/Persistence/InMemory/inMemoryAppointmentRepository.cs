using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Infrastructure.Persistence.InMemory;

public class InMemoryAppointmentRepository : IAppointmentRepository
{
    private static readonly List<Appointment> _appointments = new();

    public Task AddAsync(Appointment appointment, CancellationToken ct)
    {
        _appointments.Add(appointment);
        return Task.CompletedTask;
    }

    public Task<bool> HasOverlapAsync(Guid employeeId, DateTime startAt, DateTime endAt, CancellationToken ct)
    {
        var overlap = _appointments.Any(a =>
            a.EmployeeId == employeeId &&
            a.Status != Domain.Enums.AppointmentStatus.Cancelled &&
            startAt < a.EndAt &&
            endAt > a.StartAt
        );

        return Task.FromResult(overlap);
    }
}
