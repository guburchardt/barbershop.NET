using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Appointments.CreateAppointment;

public class CreateAppointmentHandler
{
    private readonly IAppointmentRepository _appointments;
    private readonly ITimeBlockRepository _timeBlocks;
    private readonly IEmployeeRepository _employees;
    private readonly IClientRepository _clients;

    public CreateAppointmentHandler(IAppointmentRepository appointments, ITimeBlockRepository timeBlocks, IEmployeeRepository employees, IClientRepository clients)
    {
        _appointments = appointments;
        _timeBlocks = timeBlocks;
        _employees = employees;
        _clients = clients;
    }

    public async Task<Appointment> Handle(CreateAppointmentCommand cmd, CancellationToken ct)
    {
        // Appointment in the past
        if (cmd.StartAt <= DateTime.UtcNow)
            throw new InvalidOperationException("Cannot book an appointment in the past.");

        if (cmd.EndAt <= DateTime.UtcNow)
            throw new InvalidOperationException("Cannot book an appointment in the past.");

        // Employee exists and is active
        var employee = await _employees.GetByIdAsync(cmd.EmployeeId, ct);
        if (employee is null)
            throw new InvalidOperationException("Employee not found.");

        if (!employee.IsActive)
            throw new InvalidOperationException("Employee is inactive.");
        
        // Client exists and is active
        var client = await _clients.GetByIdAsync(cmd.ClientId, ct);
        if (client is null)
            throw new InvalidOperationException("Client not found.");

        if (!client.IsActive)
            throw new InvalidOperationException("Client is inactive.");

        // Disponibility rule: conclicts with blocks
        var blocked = await _timeBlocks.HasOverlapAsync(cmd.EmployeeId, cmd.StartAt, cmd.EndAt, ct);
        if (blocked)
            throw new InvalidOperationException("This time is blocked.");
        
        var overlaps = await _appointments.HasOverlapAsync(cmd.EmployeeId, cmd.StartAt, cmd.EndAt, ct);
        if (overlaps)
            throw new InvalidOperationException("This time is not available for this employee");

        var appointment = new Appointment(cmd.EmployeeId, cmd.ClientId, cmd.StartAt, cmd.EndAt);

        await _appointments.AddAsync(appointment, ct);

        return appointment;
    }
}
