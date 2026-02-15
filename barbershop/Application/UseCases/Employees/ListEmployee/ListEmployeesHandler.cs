using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Employees.ListEmployee;

public class ListEmployeesHandler
{
    private readonly IEmployeeRepository _employees;

    public ListEmployeesHandler (IEmployeeRepository employees)
    {
        _employees = employees;
    }

    public Task <IReadOnlyList<Employee>> Handle(ListEmployeesQuery query, CancellationToken ct)
        => _employees.GetAllAsync(ct);
}
