using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Services.ActivateService;

public class ActivateServiceHandler
{
    private readonly IServiceRepository _services;

    public ActivateServiceHandler (IServiceRepository services)
    {
        _services = services;
    }

    public async Task<Service?> Handle(ActivateServiceCommand cmd, CancellationToken ct)
    {
        var service = await _services.GetByIdAsync(cmd.Id, ct);
        if (service is null) return null;

        service.Activate();

        await _services.UpdateAsync(service, ct);
        return service;
    }
}
