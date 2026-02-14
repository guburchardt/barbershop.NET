using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Employees.CreateEmployee;

public class CreateEmployeeHandler
{
    private readonly IEmployeeRepository _employees;

    public CreateEmployeeHandler (IEmployeeRepository employees)
    {
        _employees = employees;
    }

    public async Task<Employee> Handle(CreateEmployeeCommand cmd, CancellationToken ct)
    {
        var employee = new Employee(cmd.FullName);

        await _employees.AddAsync(employee, ct);

        return employee;
    }
}
