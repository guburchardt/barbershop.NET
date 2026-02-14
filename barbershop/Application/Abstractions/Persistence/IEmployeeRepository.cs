using System;
using barbershop.Domain.Entities;

namespace barbershop.Application.Abstractions.Persistence;

public interface IEmployeeRepository
{
    Task AddAsync (Employee employee, CancellationToken ct);
    Task <Employee?> GetByIdAsync (Guid id, CancellationToken ct);
    Task <IReadOnlyList<Client>> GetAllAsync (CancellationToken ct);
    Task UpdateAsync (Client client, CancellationToken ct);
}
