namespace barbershop.Application.UseCases.Availability.GetAvailability;

public record GetAvailabilityQuery(DateTime Date, Guid? EmployeeId);
