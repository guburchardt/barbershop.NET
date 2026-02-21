namespace barbershop.Contracts.Responses;

public record AppointmentResponse(
    Guid Id,
    Guid EmployeeId,
    Guid ClientId,
    Guid ServiceId,
    decimal Price,
    DateTime StartAt,
    DateTime EndAt,
    string Status,
    DateTime? CancelledAt,
    string? CancelReason
);
