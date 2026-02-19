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

    public void Cancel(DateTime nowUtc, string? reason = null)
    {
        if (Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Appointment already cancelled.");

        if (StartAt <= nowUtc)
            throw new InvalidOperationException("Cannot cancel an appointment that already started.");
        
        
        Status = AppointmentStatus.Cancelled;
        CancelledAt = nowUtc;
        CancelReason = reason;
        UpdatedAt = nowUtc;
    }

    public void Complete(DateTime nowUtc)
    {
        if (Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Cannot complete a cancelled appointment.");

        if (Status == AppointmentStatus.Completed)
            throw new InvalidOperationException("Appointment already completed.");

        Status = AppointmentStatus.Completed;
        UpdatedAt = nowUtc;
    }
}
