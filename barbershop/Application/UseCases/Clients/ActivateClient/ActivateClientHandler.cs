using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Clients.ActivateClient;

public class ActivateClientHandler
{
    private readonly IClientRepository _clients;

    public ActivateClientHandler (IClientRepository clients)
    {
        _clients = clients;
    }

    public async Task<Client?> Handle(ActivateClientCommand cmd, CancellationToken ct)
    {
        var client = await _clients.GetByIdAsync(cmd.Id, ct);
        if (client is null) return null;

        client.Activate();

        await _clients.UpdateAsync(client, ct);
        return client;
    }
}
