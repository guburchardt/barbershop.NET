using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Infrastructure.Persistence.InMemory;

public class InMemoryServiceRepository : IServiceRepository
{
    private static readonly List<Service> _services = new();

    public Task AddAsync(Service service, CancellationToken ct)
    {
        _services.Add(service);
        return Task.CompletedTask;
    }

    public Task<Service?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var service = _services.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(service);
    }

    public Task<IReadOnlyList<Service>> GetAllAsync(CancellationToken ct)
    {
        // Retorna apenas os ativos como IReadOnlyList
        IReadOnlyList<Service> result = _services.Where(s => s.IsActive).ToList();
        return Task.FromResult(result);
    }

    public Task UpdateAsync(Service service, CancellationToken ct)
    {
        // Em um banco de dados real, aqui faríamos o Update.
        // Como é em memória e usamos a mesma referência, não precisamos fazer nada,
        // mas o método precisa existir para quando pularmos para o EF Core (Fase 1.2).
        return Task.CompletedTask;
    }
}
