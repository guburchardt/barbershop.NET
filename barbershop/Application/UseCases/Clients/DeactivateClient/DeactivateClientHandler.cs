using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Clients.DeactivateClient;

public class DeactivateClientHandler
{
    private readonly IClientRepository _clients;

    public DeactivateClientHandler (IClientRepository clients)
    {
        _clients = clients;
    }

    public async Task <Client?> Handle(DeactivateClientCommand cmd, CancellationToken ct)
    {
        var client = await _clients.GetByIdAsync(cmd.Id, ct);
        if (client is null)
            return null;
        
        client.Deactivate();

        await _clients.UpdateAsync(client, ct);
        return client;
    }
}
