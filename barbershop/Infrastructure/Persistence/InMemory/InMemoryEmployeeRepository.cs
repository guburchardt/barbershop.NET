using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Infrastructure.Persistence.InMemory;

public class InMemoryEmployeeRepository : IEmployeeRepository
{
    private static readonly List<Employee> _employees = new();

    public Task AddAsync(Employee employee, CancellationToken ct)
    {
        _employees.Add(employee);
        return Task.CompletedTask;
    }

    public Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(employee);
    }

    public Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken ct)
        => Task.FromResult((IReadOnlyList<Employee>)_employees.ToList());

    public Task UpdateAsync(Employee employee, CancellationToken ct)
    {
        // in-memory: a referência já está atualizada
        return Task.CompletedTask;
    }
}
