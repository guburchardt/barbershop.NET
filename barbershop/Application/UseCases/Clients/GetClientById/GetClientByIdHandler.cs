using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Clients.GetClientById;

public class GetClientByIdHandler
{
    private readonly IClientRepository _clients;

    public GetClientByIdHandler (IClientRepository clients)
    {
        _clients = clients;
    }

    public Task<Client?> Handle(GetClientByIdQuery query, CancellationToken ct)
        => _clients.GetByIdAsync(query.Id, ct);
}
