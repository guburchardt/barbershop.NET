namespace barbershop.Application.UseCases.Services.UpdateService;

public record UpdateServiceCommand
(
    Guid Id,
    string? Name,
    string? Description,
    decimal? Price,
    int? DurationInMinutes
);
