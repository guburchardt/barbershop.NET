namespace barbershop.Contracts.Responses;

public record ClientResponse(
    Guid Id,
    string FullName,
    bool IsActive
);
