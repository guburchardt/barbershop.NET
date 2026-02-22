namespace barbershop.Application.UseCases.Services.CreateService;

public record CreateServiceCommand
(
    string Name,
    string? Description,
    decimal Price,
    int DurationInMinutes
);
