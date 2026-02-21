using barbershop.Application.UseCases.Services.CreateService;
using barbershop.Contracts.Requests;
using barbershop.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace barbershop.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly CreateServiceHandler _create;

    public ServiceController(CreateServiceHandler create)
    {
        _create = create;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceRequest request, CancellationToken ct)
    {
        var command = new CreateServiceCommand(
            request.Name,
            request.Description,
            request.Price,
            request.DurationInMinutes
        );

        var service = await _create.Handle(command, ct);

        var response = new ServiceResponse(
            service.Id,
            service.Name,
            service.Description,
            service.Price,
            service.DurationInMinutes,
            service.IsActive
        );

        return Ok(response);
    }
}
