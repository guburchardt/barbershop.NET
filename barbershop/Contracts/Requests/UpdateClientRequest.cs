using System;

namespace barbershop.Contracts.Requests;

public class UpdateClientRequest
{
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}
