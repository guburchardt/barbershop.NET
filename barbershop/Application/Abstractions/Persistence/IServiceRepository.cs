using barbershop.Domain.Entities;

namespace barbershop.Application.Abstractions.Persistence;

public interface IServiceRepository
{
    Task AddAsync (Service service, CancellationToken ct);
    Task <Service?> GetByIdAsync (Guid id, CancellationToken ct);
    Task <IReadOnlyList<Service>> GetAllAsync (CancellationToken ct);
    Task UpdateAsync (Service service, CancellationToken ct);
}
