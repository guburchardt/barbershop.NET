namespace barbershop.Application.UseCases.Clients.CreateClient;

public record CreateClientCommand
(
    string FullName,
    string? Phone,
    string? Email
);
