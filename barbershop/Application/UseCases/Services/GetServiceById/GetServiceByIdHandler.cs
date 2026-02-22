using barbershop.Application.Abstractions.Persistence;
using barbershop.Domain.Entities;

namespace barbershop.Application.UseCases.Services.GetServiceById;

public class GetServiceByIdHandler
{
    private readonly IServiceRepository _services;

    public GetServiceByIdHandler(IServiceRepository services)
    {
        _services = services;
    }

    public Task<Service?> Handle(GetServiceByIdQuery query, CancellationToken ct)
        => _services.GetByIdAsync(query.Id, ct);
}
