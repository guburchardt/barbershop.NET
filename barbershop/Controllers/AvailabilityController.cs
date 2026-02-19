using barbershop.Application.UseCases.Availability.GetAvailability;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace barbershop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly GetAvailabilityHandler _handler;

        public AvailabilityController(GetAvailabilityHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? date, [FromQuery] Guid? employeeId, CancellationToken ct)
        {
            var day = ( date ?? DateTime.UtcNow).Date;

            var response = await _handler.Handle(new GetAvailabilityQuery(day, employeeId), ct);

            return Ok(response);
        }
    }
}
