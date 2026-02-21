namespace barbershop.Contracts.Requests;

public class UpdateServiceRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? DurationInMinutes {get; set; }
}
