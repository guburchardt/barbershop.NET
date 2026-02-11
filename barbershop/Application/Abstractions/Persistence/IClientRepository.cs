using barbershop.Domain.Entities;

namespace barbershop.Application.Abstractions.Persistence;

public interface IClientRepository
{
    Task AddAsync (Client client, CancellationToken ct);
    Task <Client?> GetByIdAsync (Guid id, CancellationToken ct);
    Task <bool> ExistsByEmailAsync (string email, CancellationToken ct);
    Task <bool> ExistsByPhoneAsync (string phone, CancellationToken ct);
}
