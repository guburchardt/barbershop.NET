namespace barbershop.Application.UseCases.Appointments.CreateAppointment;

public record CreateAppointmentCommand
(
    Guid EmployeeId,
    Guid ClientId,
    Guid ServiceId,
    DateTime StartAt,
    DateTime EndAt
);

