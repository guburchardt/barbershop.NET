using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Services.UpdateService;

public class UpdateServiceHandler
{
    private readonly IServiceRepository _services;

    public UpdateServiceHandler(IServiceRepository services)
    {
        _services = services;
    }

    public async Task<Service?> Handle(UpdateServiceCommand cmd, CancellationToken ct)
    {
        var service = await _services.GetByIdAsync(cmd.Id, ct);
        if (service is null) return null;

        var nameToUpdate = !string.IsNullOrWhiteSpace(cmd.Name) ? cmd.Name : service.Name;
        var descriptionToUpdate = cmd.Description ?? service.Description;
        var priceToUpdate = cmd.Price ?? service.Price;
        var durationToUpdate = cmd.DurationInMinutes ?? service.DurationInMinutes;

        service.UpdateDetails(nameToUpdate, descriptionToUpdate, priceToUpdate, durationToUpdate);

        await _services.UpdateAsync(service, ct);

        return service;
    }
}
