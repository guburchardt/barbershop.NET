using barbershop.Application.Abstractions.Persistence;
using barbershop.Infrastructure.Persistence.InMemory;
using barbershop.Application.UseCases.Appointments.CreateAppointment;
using barbershop.Application.UseCases.Clients.CreateClient;
using barbershop.Application.UseCases.Clients.ListClients;
using barbershop.Application.UseCases.Clients.GetClientById;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAppointmentRepository, InMemoryAppointmentRepository>();
builder.Services.AddSingleton<ITimeBlockRepository, InMemoryTimeBlockRepository>();
builder.Services.AddSingleton<IClientRepository, InMemoryClientRepository>();
builder.Services.AddScoped<CreateAppointmentHandler>();
builder.Services.AddScoped<CreateClientHandler>();
builder.Services.AddScoped<ListClientsHandler>();
builder.Services.AddScoped<GetClientByIdHandler>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
