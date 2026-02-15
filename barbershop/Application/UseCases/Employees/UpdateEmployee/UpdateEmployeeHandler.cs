using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Employees.UpdateEmployee;

public class UpdateEmployeeHandler
{
    private readonly IEmployeeRepository _employees;

    public UpdateEmployeeHandler (IEmployeeRepository employees)
    {
        _employees = employees;
    }

    public async Task<Employee?> Handle(UpdateEmployeeCommand cmd, CancellationToken ct)
    {
        var employee = await _employees.GetByIdAsync(cmd.Id, ct);
        if (employee is null) return null;

        if (!string.IsNullOrWhiteSpace(cmd.FullName))
            employee.Rename(cmd.FullName);

        await _employees.UpdateAsync(employee, ct);
        return employee;
    }
}
