using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Employees.DeactivateEmployee;

public class DeactivateEmployeeHandler
{
    private readonly IEmployeeRepository _employees;

    public DeactivateEmployeeHandler (IEmployeeRepository employees)
    {
        _employees = employees;
    }

    public async Task <Employee?> Handle(DeactivateEmployeeCommand cmd, CancellationToken ct)
    {
        var employee = await _employees.GetByIdAsync(cmd.Id, ct);
        if (employee is null) return null;

        employee.Deactivate();

        await _employees.UpdateAsync(employee, ct);
        return employee;
    }

}
