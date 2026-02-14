using System;

namespace barbershop.Contracts.Requests;

public class CreateClientRequest
{
    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
}
