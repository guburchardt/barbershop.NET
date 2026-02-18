using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Appointments.CancelAppointment;

public class CancelAppointmentHandler
{
    private readonly IAppointmentRepository _appointments;

    public CancelAppointmentHandler (IAppointmentRepository appointments)
    {
        _appointments = appointments;
    }

    public async Task <Appointment?> Handle(CancelAppointmentCommand cmd, CancellationToken ct)
    {
        var appointment = await _appointments.GetByIdAsync(cmd.Id, ct);
        if (appointment is null) return null;

        appointment.Cancel(cmd.CancelReason);

        await _appointments.UpdateAsync(appointment, ct);

        return appointment;
    }
}
