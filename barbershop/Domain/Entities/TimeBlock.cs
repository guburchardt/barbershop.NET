using System;

namespace barbershop.Domain.Entities;

public class TimeBlock
{
    public Guid Id { get; private set; }
    // if null, block all employees
    public Guid? EmployeeId { get; private set; }
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }
    public string Reason { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    //Future ORM
    protected TimeBlock() { }

    public TimeBlock(DateTime startAt, DateTime endAt, string reason, Guid? employeeId = null)
    {
        if (endAt <= startAt)
            throw new ArgumentException("EndAt must be grater than StartAt.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Reason is required.");

        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        StartAt = startAt;
        EndAt = endAt;
        Reason = reason;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(DateTime startAt, DateTime endAt, string reason, Guid? employeeId)
    {

        if (endAt <= startAt)
            throw new ArgumentException("EndAt must be greater than StartAt.");
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Reason is required.");

        StartAt = startAt;
        EndAt = endAt;
        Reason = reason;
        EmployeeId = employeeId;
        UpdatedAt = DateTime.UtcNow;
    }
}
