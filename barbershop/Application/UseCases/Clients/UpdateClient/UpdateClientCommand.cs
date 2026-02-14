namespace barbershop.Application.UseCases.Clients.UpdateClient;

public record UpdateClientCommand
(
    Guid Id,
    string? FullName,
    string? Phone,
    string? Email
);
