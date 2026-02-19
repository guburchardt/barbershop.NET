namespace barbershop.Application.UseCases.Appointments.CancelAppointment;

public record CancelAppointmentCommand
(
    Guid Id,
    string? CancelReason
);
