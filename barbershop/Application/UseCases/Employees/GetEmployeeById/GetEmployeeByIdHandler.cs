using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Employees.GetEmployeeById;

public class GetEmployeeByIdHandler
{
    private readonly IEmployeeRepository _employees;

    public GetEmployeeByIdHandler (IEmployeeRepository employees)
    {
        _employees = employees;
    }

    public Task <Employee?> Handle(GetEmployeeByIdQuery query, CancellationToken ct)
        => _employees.GetByIdAsync(query.Id, ct);
}
