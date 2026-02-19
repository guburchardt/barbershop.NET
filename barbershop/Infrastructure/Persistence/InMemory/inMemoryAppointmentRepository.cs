using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;
using barbershop.Domain.Enums;

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

    public Task<Appointment?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(appointment);
    }

    public Task UpdateAsync(Appointment appointment, CancellationToken ct)
    {
        // InMemory: o objeto já está atualizado por referência
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Appointment>> ListByDayAsync(Guid? employeeId, DateTime day, string? status, CancellationToken ct)
    {
        var start = day.Date;
        var end = start.AddDays(1);

        var query = _appointments.Where(a =>
            a.StartAt < end &&
            a.EndAt > start
        );

        if (employeeId.HasValue)
            query = query.Where(a => a.EmployeeId == employeeId.Value);

        if (!string.IsNullOrWhiteSpace(status))
        {
            // tenta converter "Booked" / "Cancelled" para enum
            if (Enum.TryParse<AppointmentStatus>(status, ignoreCase: true, out var parsed))
                query = query.Where(a => a.Status == parsed);
            else
                query = Enumerable.Empty<Appointment>(); // status inválido => retorna vazio (simples por enquanto)
        }

        var result = query
            .OrderBy(a => a.StartAt)
            .ToList();

        return Task.FromResult((IReadOnlyList<Appointment>)result);
    }

    public Task<IReadOnlyList<Appointment>> ListByEmployeeAndDayAsync(Guid employeeId, DateTime day, CancellationToken ct)
    {
        var start = day.Date;
        var end = start.AddDays(1);

        var result = _appointments
            .Where(a =>
                a.EmployeeId == employeeId &&
                a.StartAt < end &&
                a.EndAt > start
            )
            .OrderBy(a => a.StartAt)
            .ToList();

        return Task.FromResult((IReadOnlyList<Appointment>)result);
    }
}
