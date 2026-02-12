using barbershop.Application.UseCases.Clients.CreateClient;
using barbershop.Contracts.Requests;
using barbershop.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace barbershop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly CreateClientHandler _handler;

        public ClientsController(CreateClientHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] CreateClientRequest request, CancellationToken ct)
        {
            var command = new CreateClientCommand(
                request.FullName,
                request.Phone,
                request.Email
            );

            var client = await _handler.Handle(command, ct);

            return Ok(new ClientResponse(
                client.Id,
                client.FullName,
                client.IsActive
            ));
        }
    }
}
