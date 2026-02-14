using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Clients.UpdateClient;

public class UpdateClientHandler
{
    private readonly IClientRepository _clients;

    public UpdateClientHandler (IClientRepository clients)
    {
        _clients = clients;
    }

    public async Task<Client?> Handle(UpdateClientCommand cmd, CancellationToken ct)
    {
        var client = await _clients.GetByIdAsync(cmd.Id, ct);
        if (client is null)
            return null;

        if (!string.IsNullOrWhiteSpace(cmd.Email))
        {
            var emailExists = await _clients.ExistsByEmailAsync(cmd.Email, ct);
            var sameEmail = client.Email != null &&
                            string.Equals(client.Email, cmd.Email, StringComparison.OrdinalIgnoreCase);

            if (emailExists && !sameEmail)
                throw new InvalidOperationException("Email already in use.");
        }

        if (!string.IsNullOrWhiteSpace(cmd.Phone))
        {
            var phoneExists = await _clients.ExistsByPhoneAsync(cmd.Phone, ct);
            var samePhone = client.Phone != null && client.Phone == cmd.Phone;

            if (phoneExists && !samePhone)
                throw new InvalidOperationException("Phone already in use.");
        }

        if (!string.IsNullOrWhiteSpace(cmd.FullName))
            client.Rename(cmd.FullName);

        if (cmd.Phone != null || cmd.Email != null)
            client.UpdateContact(cmd.Phone ?? client.Phone, cmd.Email ?? client.Email);

        await _clients.UpdateAsync(client, ct);
        return client;
    }
}
