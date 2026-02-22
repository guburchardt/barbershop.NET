using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.TimeBlocks.CreateTimeBlock;

public class CreateTimeBlockHandler
{
    private readonly ITimeBlockRepository _timeBlocks;
    private readonly IEmployeeRepository _employees;
    private readonly IAppointmentRepository _appointments;

    public CreateTimeBlockHandler(
        ITimeBlockRepository timeBlocks,
        IEmployeeRepository employees,
        IAppointmentRepository appointments)
    {
        _timeBlocks = timeBlocks;
        _employees = employees;
        _appointments = appointments;
    }

    public async Task<TimeBlock> Handle(CreateTimeBlockCommand cmd, CancellationToken ct)
    {
        if (cmd.EmployeeId.HasValue)
        {
            var employee = await _employees.GetByIdAsync(cmd.EmployeeId.Value, ct);
            if (employee is null)
                throw new InvalidOperationException("Employee not found.");

            if (!employee.IsActive)
                throw new InvalidOperationException("Employee is inactive.");
        }

        if (cmd.EndAt <= DateTime.UtcNow)
            throw new InvalidOperationException("Cannot create a time block in the past.");

        var overlap = await _timeBlocks.HasOverlapAsync(cmd.EmployeeId, cmd.StartAt, cmd.EndAt, ct);
        if (overlap)
            throw new InvalidOperationException("This time is already blocked by another block.");

        if (cmd.EmployeeId.HasValue)
        {
            var hasAppointment = await _appointments.HasOverlapAsync(cmd.EmployeeId.Value, cmd.StartAt, cmd.EndAt, ct);
            if (hasAppointment)
                throw new InvalidOperationException("Cannot block time: An appointment already exists in this period.");
        }

        var block = new TimeBlock(cmd.StartAt, cmd.EndAt, cmd.Reason, cmd.EmployeeId);

        await _timeBlocks.AddAsync(block, ct);

        return block;
    }
}
