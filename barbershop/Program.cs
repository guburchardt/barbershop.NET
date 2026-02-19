using barbershop.Application.Abstractions.Persistence;
using barbershop.Infrastructure.Persistence.InMemory;
using barbershop.Application.UseCases.Appointments.CreateAppointment;
using barbershop.Application.UseCases.Clients.CreateClient;
using barbershop.Application.UseCases.Clients.ListClients;
using barbershop.Application.UseCases.Clients.GetClientById;
using barbershop.Application.UseCases.Clients.UpdateClient;
using barbershop.Application.UseCases.Clients.DeactivateClient;
using barbershop.Application.UseCases.Clients.ActivateClient;
using barbershop.Application.UseCases.Employees.CreateEmployee;
using barbershop.Application.UseCases.Employees.ListEmployee;
using barbershop.Application.UseCases.Employees.GetEmployeeById;
using barbershop.Application.UseCases.Employees.UpdateEmployee;
using barbershop.Application.UseCases.Employees.DeactivateEmployee;
using barbershop.Application.UseCases.Employees.ActivateEmployee;
using barbershop.Application.UseCases.Appointments.CancelAppointment;
using barbershop.Application.UseCases.Appointments.GetAppointmentById;
using barbershop.Application.UseCases.Appointments.ListAppointments;
using barbershop.Application.UseCases.Appointments.CompleteAppointment;
using barbershop.Application.UseCases.Availability.GetAvailability;
using barbershop.Application.UseCases.TimeBlocks.CreateTimeBlock;
using barbershop.Application.UseCases.TimeBlocks.ListTimeBlock;
using barbershop.Application.UseCases.TimeBlocks.DeleteTimeBlock;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAppointmentRepository, InMemoryAppointmentRepository>();
builder.Services.AddSingleton<ITimeBlockRepository, InMemoryTimeBlockRepository>();
builder.Services.AddSingleton<IClientRepository, InMemoryClientRepository>();
builder.Services.AddSingleton<IEmployeeRepository, InMemoryEmployeeRepository>();
builder.Services.AddScoped<CreateAppointmentHandler>();
builder.Services.AddScoped<CreateClientHandler>();
builder.Services.AddScoped<ListClientsHandler>();
builder.Services.AddScoped<GetClientByIdHandler>();
builder.Services.AddScoped<UpdateClientHandler>();
builder.Services.AddScoped<DeactivateClientHandler>();
builder.Services.AddScoped<ActivateClientHandler>();
builder.Services.AddScoped<CreateEmployeeHandler>();
builder.Services.AddScoped<ListEmployeesHandler>();
builder.Services.AddScoped<GetEmployeeByIdHandler>();
builder.Services.AddScoped<UpdateEmployeeHandler>();
builder.Services.AddScoped<DeactivateEmployeeHandler>();
builder.Services.AddScoped<ActivateEmployeeHandler>();
builder.Services.AddScoped<CancelAppointmentHandler>();
builder.Services.AddScoped<GetAppointmentByIdHandler>();
builder.Services.AddScoped<ListAppointmentsHandler>();
builder.Services.AddScoped<CompleteAppointmentHandler>();
builder.Services.AddScoped<GetAvailabilityHandler>();
builder.Services.AddScoped<CreateTimeBlockHandler>();
builder.Services.AddScoped<ListTimeBlocksHandler>();
builder.Services.AddScoped<DeleteTimeBlockHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
