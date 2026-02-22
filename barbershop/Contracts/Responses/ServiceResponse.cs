namespace barbershop.Contracts.Responses;

public record ServiceResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int DurationInMinutes,
    bool IsActive
);
