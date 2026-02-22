using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.TimeBlocks.UpdateTimeBlock;

public class UpdateTimeBlockHandler
{
    private readonly ITimeBlockRepository _timeBlocks;
    private readonly IEmployeeRepository _employees;
    private readonly IAppointmentRepository _appointments;

    public UpdateTimeBlockHandler(ITimeBlockRepository timeBlocks, IEmployeeRepository employees, IAppointmentRepository appointments)
    {
        _timeBlocks = timeBlocks;
        _employees = employees;
        _appointments = appointments;
    }

    public async Task<TimeBlock?> Handle(UpdateTimeBlockCommand cmd, CancellationToken ct)
    {
        var block = await _timeBlocks.GetByIdAsync(cmd.Id, ct);
        if (block is null) return null;

        var startAt = cmd.StartAt ?? block.StartAt;
        var endAt = cmd.EndAt ?? block.EndAt;
        var reason = cmd.Reason ?? block.Reason;
        var employeeId = cmd.EmployeeId ?? block.EmployeeId; ;

        if (employeeId.HasValue)
        {
            var employee = await _employees.GetByIdAsync(employeeId.Value, ct);
            if (employee is null) throw new InvalidOperationException("Employee not found.");
        }

        var blockOverlap = await _timeBlocks.HasOverlapAsync(employeeId, startAt, endAt, ct, cmd.Id);
        if (blockOverlap) throw new InvalidOperationException("This time is already blocked by another block.");

        if (employeeId.HasValue)
        {
            var hasAppointment = await _appointments.HasOverlapAsync(employeeId.Value, startAt, endAt, ct);
            if (hasAppointment) throw new InvalidOperationException("Cannot block time: An appointment already exists in this period");
        }

        block.UpdateDetails(startAt, endAt, reason, employeeId);

        await _timeBlocks.UpdateAsync(block, ct);
        return block;
    }
}
