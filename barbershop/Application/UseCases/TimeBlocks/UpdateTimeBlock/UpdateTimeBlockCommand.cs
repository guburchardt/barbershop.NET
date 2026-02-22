namespace barbershop.Application.UseCases.TimeBlocks.UpdateTimeBlock;

public record UpdateTimeBlockCommand
(
    Guid Id,
    DateTime? StartAt,
    DateTime? EndAt,
    string? Reason,
    Guid? EmployeeId
);
