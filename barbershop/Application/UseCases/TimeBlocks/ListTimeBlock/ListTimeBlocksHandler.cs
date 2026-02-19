using System;
using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.TimeBlocks.ListTimeBlock;

public class ListTimeBlocksHandler
{
    private readonly ITimeBlockRepository _timeBlocks;

    public ListTimeBlocksHandler (ITimeBlockRepository timeBlocks)
    {
        _timeBlocks = timeBlocks;
    }

    public Task<IReadOnlyList<TimeBlock>> Handle(ListTimeBlocksQuery query, CancellationToken ct)
        => _timeBlocks.ListByEmployeeAndDayAsync(query.EmployeeId, query.Day, ct);
}
