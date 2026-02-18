using System;

namespace barbershop.Contracts.Requests;

public class CancelAppointmentRequest
{
    public string? CancelReason { get; set; }
}
