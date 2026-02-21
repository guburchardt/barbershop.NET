using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Services.DeactivateService;

public class DeactivateServiceHandler
{
    private readonly IServiceRepository _services;

    public DeactivateServiceHandler (IServiceRepository services)
    {
        _services = services;
    }

    public async Task <Service?> Handle(DeactivateServiceCommand cmd, CancellationToken ct)
    {
        var service = await _services.GetByIdAsync(cmd.Id, ct);
        if (service is null) return null;

        service.Deactivate();

        await _services.UpdateAsync(service, ct);
        return service;
    }
}
