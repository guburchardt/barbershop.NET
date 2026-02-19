using System;

namespace barbershop.Contracts.Requests;

public class CreateTimeBlockRequest
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public string Reason { get; set; } = string.Empty;
    public Guid? EmployeeId { get; set; }
}
