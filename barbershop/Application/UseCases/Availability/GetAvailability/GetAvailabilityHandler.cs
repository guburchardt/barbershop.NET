using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Contracts.Responses;
using barbershop.Domain.Enums;

namespace barbershop.Application.UseCases.Availability.GetAvailability;

public class GetAvailabilityHandler
{
    private readonly IEmployeeRepository _employees;
    private readonly IAppointmentRepository _appointments;
    private readonly ITimeBlockRepository _timeBlocks;

    public GetAvailabilityHandler(IEmployeeRepository employees, IAppointmentRepository appointments, ITimeBlockRepository timeBlocks)
    {
        _employees = employees;
        _appointments = appointments;
        _timeBlocks = timeBlocks;
    }

    public async Task<AvailabilityResponse> Handle(GetAvailabilityQuery query, CancellationToken ct)
    {
        var day = query.Date.Date;

        // MVP
        var workStart = day.AddHours(9);
        var workEnd = day.AddHours(18);

        var slotMinutes = 30;

        // Choose employee
        IReadOnlyList<barbershop.Domain.Entities.Employee> employees;
        if (query.EmployeeId.HasValue)
        {
            var emp = await _employees.GetByIdAsync(query.EmployeeId.Value, ct);
            employees = emp is null ? Array.Empty<barbershop.Domain.Entities.Employee>() : new[] { emp };
        }
        else
        {
            employees = await _employees.GetAllAsync(ct);
        }

        // only actives
        employees = employees.Where(e => e.IsActive).ToList();

        var result = new List<EmployeeAvailabilityResponse>();

        foreach (var emp in employees)
        {
            var apps = await _appointments.ListByEmployeeAndDayAsync(emp.Id, day, ct);
            var bookedIntervals = apps
                .Where(a => a.Status == AppointmentStatus.Booked)
                .Select(a => (a.StartAt, a.EndAt))
                .ToList();

            var blocks = await _timeBlocks.ListByEmployeeAndDayAsync(emp.Id, day, ct);
            var blockIntervals = blocks
                .Select(b => (b.StartAt, b.EndAt))
                .ToList();

            var busy = bookedIntervals.Concat(blockIntervals).ToList();

            var slots = new List<AvailableSlotResponse>();
            for (var t = workStart; t.AddMinutes(slotMinutes) <= workEnd; t = t.AddMinutes(slotMinutes))
            {
                var slotStart = t;
                var slotEnd = t.AddMinutes(slotMinutes);

                var overlaps = busy.Any(b => slotStart < b.EndAt && slotEnd > b.StartAt);
                if (!overlaps)
                    slots.Add(new AvailableSlotResponse(slotStart, slotEnd));
            }

            result.Add(new EmployeeAvailabilityResponse(emp.Id, slots));
        }

        return new AvailabilityResponse(day, result);
    }
}
