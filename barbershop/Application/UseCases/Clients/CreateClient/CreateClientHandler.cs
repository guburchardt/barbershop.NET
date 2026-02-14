using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Clients.CreateClient;

public record CreateClientHandler
{
    private readonly IClientRepository _clients;

    public CreateClientHandler (IClientRepository clients)
    {
        _clients = clients;
    }

    public async Task <Client> Handle(CreateClientCommand cmd, CancellationToken ct)
    {
        if (!string.IsNullOrWhiteSpace(cmd.Email))
        {
            var emailExists = await _clients.ExistsByEmailAsync(cmd.Email, ct);
            if (emailExists)
                throw new InvalidOperationException("Email already in use.");
        }

        if (!string.IsNullOrWhiteSpace(cmd.Phone))
        {
            var phoneExists = await _clients.ExistsByPhoneAsync(cmd.Phone, ct);
            if (phoneExists)
                throw new InvalidOperationException("Phone already in use.");
        }

        var client = new Client (cmd.FullName, cmd.Phone, cmd.Email);

        await _clients.AddAsync (client, ct);

        return client;
    }
}
