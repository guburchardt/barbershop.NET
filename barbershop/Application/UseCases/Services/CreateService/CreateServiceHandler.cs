using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Services.CreateService;

public class CreateServiceHandler
{
    private readonly IServiceRepository _services;

    public CreateServiceHandler (IServiceRepository services)
    {
        _services = services;
    }

    public async Task<Service> Handle(CreateServiceCommand cmd, CancellationToken ct)
    {
        var service = new Service (
            cmd.Name,
            cmd.Description,
            cmd.Price,
            cmd.DurationInMinutes
        );

        await _services.AddAsync(service, ct);

        return service;
    }
}
