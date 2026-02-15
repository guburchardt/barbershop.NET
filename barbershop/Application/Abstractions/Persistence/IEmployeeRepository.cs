using System;
using barbershop.Domain.Entities;

namespace barbershop.Application.Abstractions.Persistence;

public interface IEmployeeRepository
{
    Task AddAsync (Employee employee, CancellationToken ct);
    Task <Employee?> GetByIdAsync (Guid id, CancellationToken ct);
    Task <IReadOnlyList<Employee>> GetAllAsync (CancellationToken ct);
    Task UpdateAsync (Employee employee, CancellationToken ct);
}
