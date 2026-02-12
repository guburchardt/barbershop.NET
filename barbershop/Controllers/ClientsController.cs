using barbershop.Application.UseCases.Clients.CreateClient;
using barbershop.Application.UseCases.Clients.ListClients;
using barbershop.Application.UseCases.Clients.GetClientById;
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
        private readonly CreateClientHandler _create;
        private readonly ListClientsHandler _list;
        private readonly GetClientByIdHandler _getById;

        public ClientsController(CreateClientHandler create, ListClientsHandler list, GetClientByIdHandler getById)
        {
            _create = create;
            _list = list;
            _getById = getById;
        }

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] CreateClientRequest request, CancellationToken ct)
        {
            var command = new CreateClientCommand(
                request.FullName,
                request.Phone,
                request.Email
            );

            var client = await _create.Handle(command, ct);

            return Ok(new ClientResponse(
                client.Id,
                client.FullName,
                client.IsActive
            ));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var clients = await _list.Handle(new ListClientsQuery(), ct);
            var response = clients.Select(c => new ClientResponse(c.Id, c.FullName, c.IsActive));
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var client = await _getById.Handle(new GetClientByIdQuery(id), ct);
            if (client is null) return NotFound();

            return Ok(new ClientResponse(client.Id, client.FullName, client.IsActive));
        }
    }
}
