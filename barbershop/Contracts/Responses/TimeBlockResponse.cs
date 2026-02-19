namespace barbershop.Contracts.Responses;

public record TimeBlockResponse
(
    Guid Id,
    Guid? EmployeeId,
    DateTime StartAt,
    DateTime EndAt,
    string Reason
);
