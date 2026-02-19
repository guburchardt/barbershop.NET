using barbershop.Application.UseCases.TimeBlocks.CreateTimeBlock;
using barbershop.Application.UseCases.TimeBlocks.DeleteTimeBlock;
using barbershop.Application.UseCases.TimeBlocks.ListTimeBlock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using barbershop.Contracts.Requests;
using barbershop.Contracts.Responses;


namespace barbershop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeBlocksController : ControllerBase
    {
        private readonly CreateTimeBlockHandler _create;
        private readonly ListTimeBlocksHandler _list;
        private readonly DeleteTimeBlockHandler _delete;

        public TimeBlocksController(
            CreateTimeBlockHandler create,
            ListTimeBlocksHandler list,
            DeleteTimeBlockHandler delete
        )
        {
            _create = create;
            _list = list;
            _delete = delete;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTimeBlockRequest request, CancellationToken ct)
        {
            var block = await _create.Handle(new CreateTimeBlockCommand(
                request.StartAt,
                request.EndAt,
                request.Reason,
                request.EmployeeId
            ), ct);

            return Ok(new TimeBlockResponse(
                block.Id,
                block.EmployeeId,
                block.StartAt,
                block.EndAt,
                block.Reason
            ));
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] Guid employeeId, [FromQuery] DateTime? date, CancellationToken ct)
        {
            var day = (date ?? DateTime.UtcNow).Date;

            var blocks = await _list.Handle(new ListTimeBlocksQuery(employeeId, day), ct);

            var response = blocks.Select(b => new TimeBlockResponse(
                b.Id, b.EmployeeId, b.StartAt, b.EndAt, b.Reason
            ));

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var deleted = await _delete.Handle(new DeleteTimeBlockCommand(id), ct);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
