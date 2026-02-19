using System;
using barbershop.Application.Abstractions.Persistence;

namespace barbershop.Application.UseCases.TimeBlocks.DeleteTimeBlock;

public class DeleteTimeBlockHandler
{
    private readonly ITimeBlockRepository _timeBlocks;

    public DeleteTimeBlockHandler (ITimeBlockRepository timeBlocks)
    {
        _timeBlocks = timeBlocks;
    }

    public async Task<bool> Handle(DeleteTimeBlockCommand cmd, CancellationToken ct)
    {
        var block = await _timeBlocks.GetByIdAsync(cmd.Id, ct);
        if (block is null) return false;

        await _timeBlocks.RemoveAsync(block, ct);
        return true;
    }
}
