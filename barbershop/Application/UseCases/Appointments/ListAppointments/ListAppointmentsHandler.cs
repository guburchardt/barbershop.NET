using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Appointments.ListAppointments;

public class ListAppointmentsHandler
{
    private readonly IAppointmentRepository _appointments;

    public ListAppointmentsHandler (IAppointmentRepository appointments)
    {
        _appointments = appointments;
    }

    public Task<IReadOnlyList<Appointment>> Handle(ListAppointmentsQuery query, CancellationToken ct)
        => _appointments.ListByDayAsync(query.EmployeeId, query.Day, query.Status, ct);
}
