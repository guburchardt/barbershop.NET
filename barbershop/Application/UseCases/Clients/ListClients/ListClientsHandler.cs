using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Clients.ListClients;

public class ListClientsHandler
{
    private readonly IClientRepository _clients;

    public ListClientsHandler (IClientRepository clients)
    {
        _clients = clients;
    }

    public Task <IReadOnlyList<Client>> Handle(ListClientsQuery query, CancellationToken ct)
        => _clients.GetAllAsync(ct);
}
