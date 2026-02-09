namespace barbershop.Application.UseCases.Appointments.CreateAppointment;

public record CreateAppointmentCommand
(
    Guid EmployeeId,
    Guid ClientId,
    DateTime StartAt,
    DateTime EndAt
);

