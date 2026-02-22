namespace barbershop.Application.UseCases.TimeBlocks.ListTimeBlock;

public record ListTimeBlocksQuery
(
    Guid? EmployeeId,
    DateTime Day
);
