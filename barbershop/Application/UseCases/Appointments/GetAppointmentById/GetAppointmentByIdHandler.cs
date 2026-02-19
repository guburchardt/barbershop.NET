using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Appointments.GetAppointmentById;

public class GetAppointmentByIdHandler
{
    private readonly IAppointmentRepository _appointments;

    public GetAppointmentByIdHandler (IAppointmentRepository appointments)
    {
        _appointments = appointments;
    }

    public Task <Appointment?> Handle (GetAppointmentByIdQuery query, CancellationToken ct)
        => _appointments.GetByIdAsync(query.Id, ct);
}
