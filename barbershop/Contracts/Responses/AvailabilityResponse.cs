namespace barbershop.Contracts.Responses;

public record AvailabilityResponse
(
    DateTime Date,
    IReadOnlyList<EmployeeAvailabilityResponse> Employees
);
