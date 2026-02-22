using barbershop.Application.UseCases.Services.ActivateService;
using barbershop.Application.UseCases.Services.CreateService;
using barbershop.Application.UseCases.Services.DeactivateService;
using barbershop.Application.UseCases.Services.GetServiceById;
using barbershop.Application.UseCases.Services.ListService;
using barbershop.Application.UseCases.Services.UpdateService;
using barbershop.Contracts.Requests;
using barbershop.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace barbershop.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly CreateServiceHandler _create;
    private readonly ListServicesHandler _list;
    private readonly GetServiceByIdHandler _getById;
    private readonly UpdateServiceHandler _update;
    private readonly DeactivateServiceHandler _deactivate;
    private readonly ActivateServiceHandler _activate;

    public ServiceController(
        CreateServiceHandler create,
        ListServicesHandler list,
        GetServiceByIdHandler getById,
        UpdateServiceHandler update,
        DeactivateServiceHandler deactivate,
        ActivateServiceHandler activate)
    {
        _create = create;
        _list = list;
        _getById = getById;
        _update = update;
        _deactivate = deactivate;
        _activate = activate;
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var services = await _list.Handle(new ListServicesQuery(), ct);
        var response = services.Select(s => new ServiceResponse(
            s.Id,
            s.Name,
            s.Description,
            s.Price,
            s.DurationInMinutes,
            s.IsActive
        ));

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var service = await _getById.Handle(new GetServiceByIdQuery(id), ct);
        if (service is null) return NotFound();

        return Ok(new ServiceResponse(
            service.Id,
            service.Name,
            service.Description,
            service.Price,
            service.DurationInMinutes,
            service.IsActive
        ));
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateServiceRequest request, CancellationToken ct)
    {
        var cmd = new UpdateServiceCommand(
            id,
            request.Name,
            request.Description,
            request.Price,
            request.DurationInMinutes
        );

        var service = await _update.Handle(cmd, ct);
        if (service is null) return NotFound();

        return Ok(new ServiceResponse(
            service.Id,
            service.Name,
            service.Description,
            service.Price,
            service.DurationInMinutes,
            service.IsActive
        ));
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

    [HttpPost("{id:guid}/activate")]
    public async Task<IActionResult> Activate([FromRoute] Guid id, CancellationToken ct)
    {
        var service = await _activate.Handle(new ActivateServiceCommand(id), ct);
        if (service is null) return NotFound();

        return Ok(new ServiceResponse(
            service.Id,
            service.Name,
            service.Description,
            service.Price,
            service.DurationInMinutes,
            service.IsActive
        ));
    }

    [HttpDelete("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id, CancellationToken ct)
    {
        var service = await _deactivate.Handle(new DeactivateServiceCommand(id), ct);
        if (service is null) return NotFound();

        return Ok(new ServiceResponse(
            service.Id,
            service.Name,
            service.Description,
            service.Price,
            service.DurationInMinutes,
            service.IsActive
        ));
    }
}
