using System;
using barbershop.Domain.Enums;

namespace barbershop.Domain.Entities;

public class Appointment
{
    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Guid ClientId { get; private set; }
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    public string? CancelReason { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Future ORM
    protected Appointment () {}

    public Appointment (Guid employeeId, Guid clientId, DateTime startAt, DateTime endAt)
    {
        if (employeeId == Guid.Empty)
            throw new ArgumentException("EmployeeId is required.");
        
        if (clientId == Guid.Empty)
            throw new ArgumentException("ClientId is required.");

        if (endAt <= startAt)
            throw new ArgumentException("EndAt must be grater than StartAt.");

        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        ClientId = clientId;

        StartAt = startAt;
        EndAt = endAt;

        Status = AppointmentStatus.Booked;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel(string? reason = null)
    {
        if (Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Appointment already cancelled.");

        Status = AppointmentStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;
        CancelReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }

}
