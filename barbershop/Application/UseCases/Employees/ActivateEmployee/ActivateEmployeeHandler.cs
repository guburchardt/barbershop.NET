using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Employees.ActivateEmployee;

public class ActivateEmployeeHandler
{
    private readonly IEmployeeRepository _employees;

    public ActivateEmployeeHandler (IEmployeeRepository employees)
    {
        _employees = employees;
    }

    public async Task<Employee?> Handle(ActivateEmployeeCommand cmd, CancellationToken ct)
    {
        var employee = await _employees.GetByIdAsync(cmd.Id, ct);
        if (employee is null) return null;

        employee.Activate();

        await _employees.UpdateAsync(employee, ct);
        return employee;
    }
}
