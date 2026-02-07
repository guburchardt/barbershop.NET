namespace barbershop.Contracts.Responses;

public record EmployeeResponse(
    Guid Id,
    string FullName,
    bool IsActive
);
