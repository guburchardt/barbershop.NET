namespace barbershop.Application.UseCases.Appointments.ListAppointments;

public record ListAppointmentsQuery(Guid? EmployeeId, DateTime Day, string? Status);
