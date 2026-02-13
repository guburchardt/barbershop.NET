using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Infrastructure.Persistence.InMemory;

public class InMemoryClientRepository : IClientRepository
{
    private static readonly List<Client> _clients = new();

    public Task AddAsync(Client client, CancellationToken ct)
    {
        _clients.Add(client);
        return Task.CompletedTask;
    }

    public Task<Client?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(client);
    }

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken ct)
    {
        var exists = _clients.Any(c =>
            c.Email != null &&
            string.Equals(c.Email, email, StringComparison.OrdinalIgnoreCase)
        );

        return Task.FromResult(exists);
    }

    public Task<bool> ExistsByPhoneAsync(string phone, CancellationToken ct)
    {
        // Aqui é simples: compara exato. (Depois dá pra normalizar: tirar espaços, DDI, etc.)
        var exists = _clients.Any(c => c.Phone != null && c.Phone == phone);
        return Task.FromResult(exists);
    }

    public Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken ct)
    {
        return Task.FromResult((IReadOnlyList<Client>)_clients.ToList());
    }

    public Task UpdateAsync(Client client, CancellationToken ct)
    {
        // In-memory guarda a referência do objeto, então não precisa fazer nada aqui.
        return Task.CompletedTask;
    }

}
