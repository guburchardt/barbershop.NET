using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Appointments.CompleteAppointment;

public class CompleteAppointmentHandler
{
    private readonly IAppointmentRepository _appointments;

    public CompleteAppointmentHandler(IAppointmentRepository appointments)
    {
        _appointments = appointments;
    }

    public async Task<Appointment?> Handle(CompleteAppointmentCommand cmd, CancellationToken ct)
    {
        var appointment = await _appointments.GetByIdAsync(cmd.Id, ct);
        if (appointment is null) return null;

        appointment.Complete(DateTime.UtcNow);
        await _appointments.UpdateAsync(appointment, ct);

        return appointment;
    }
}
