using System;

namespace barbershop.Contracts.Requests;

public class CreateAppointmentRequest
{
    public Guid EmployeeId { get; set; }
    public Guid ClientId { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}
