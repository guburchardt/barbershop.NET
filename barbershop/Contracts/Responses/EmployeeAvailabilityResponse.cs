namespace barbershop.Contracts.Responses;

public record EmployeeAvailabilityResponse
(
    Guid EmployeeId,
    IReadOnlyList<AvailableSlotResponse> Slots
);
