namespace barbershop.Application.UseCases.Employees.UpdateEmployee;

public record UpdateEmployeeCommand
(
    Guid Id,
    string? FullName
);
