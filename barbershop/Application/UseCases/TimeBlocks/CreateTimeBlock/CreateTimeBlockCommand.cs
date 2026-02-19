namespace barbershop.Application.UseCases.TimeBlocks.CreateTimeBlock;

public record CreateTimeBlockCommand
(
    DateTime StartAt,
    DateTime EndAt,
    string Reason,
    Guid? EmployeeId
);