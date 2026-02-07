using System;

namespace barbershop.Contracts.Requests;

public class CreateEmployeeRequest
{
    public string FullName { get; set; } = string.Empty;
}
