namespace barbershop.Contracts.Requests;

public class UpdateTimeBlockRequest
{
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public string? Reason { get; set; }
    public Guid? EmployeeId { get; set; }
}
