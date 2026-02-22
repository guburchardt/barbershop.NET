using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.TimeBlocks.GetTimeBlockById;

public class GetTimeBlockByIdHandler
{
    private readonly ITimeBlockRepository _timeBlocks;

    public GetTimeBlockByIdHandler (ITimeBlockRepository timeBlocks)
    {
        _timeBlocks = timeBlocks;
    }

    public Task <TimeBlock?> Handle(GetTimeBlockByIdQuery query, CancellationToken ct)
        => _timeBlocks.GetByIdAsync(query.Id, ct);
}
