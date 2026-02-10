using barbershop.Application.Abstractions.Persistence;

namespace barbershop.Infrastructure.Persistence.InMemory;

public class InMemoryTimeBlockRepository : ITimeBlockRepository
{
    // Exemplo: lista vazia por enquanto
    public Task<bool> HasOverlapAsync(Guid employeeId, DateTime startAt, DateTime endAt, CancellationToken ct)
    {
        return Task.FromResult(false);
    }
}
