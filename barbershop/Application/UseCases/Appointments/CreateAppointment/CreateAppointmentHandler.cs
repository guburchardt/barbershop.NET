using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Appointments.CreateAppointment;

public class CreateAppointmentHandler
{
    private readonly IAppointmentRepository _appointments;
    private readonly ITimeBlockRepository _timeBlocks;

    public CreateAppointmentHandler(IAppointmentRepository appointments, ITimeBlockRepository timeBlocks)
    {
        _appointments = appointments;
        _timeBlocks = timeBlocks;
    }

    public async Task<Appointment> Handle(CreateAppointmentCommand cmd, CancellationToken ct)
    {
        // Create Entity (Validate interval and ids inside domain)
        var appointment = new Appointment(cmd.EmployeeId, cmd.ClientId, cmd.StartAt, cmd.EndAt);

        // Disponibility rule: conflicts with blocks.
        var blocked = await _timeBlocks.HasOverlapAsync(cmd.EmployeeId, cmd. StartAt, cmd.EndAt, ct);
        if (blocked)
            throw new InvalidOperationException("This time is blocked.");

        // Disponibility rule: conflict with other appointments.
        var overlaps = await _appointments.HasOverlapAsync(cmd.EmployeeId, cmd.StartAt, cmd.EndAt, ct);
        if (overlaps)
            throw new InvalidOperationException("This time is not available for this employee.");
        
        // Persist
        await _appointments.AddAsync(appointment, ct);

        return appointment;
    }
}
