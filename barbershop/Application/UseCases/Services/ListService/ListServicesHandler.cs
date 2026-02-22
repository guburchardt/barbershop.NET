using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Services.ListService;

public class ListServicesHandler
{
    private readonly IServiceRepository _services;

    public ListServicesHandler(IServiceRepository services)
    {
        _services = services;
    }

    public Task<IReadOnlyList<Service>> Handle(ListServicesQuery query, CancellationToken ct)
        => _services.GetAllAsync(ct);
}
