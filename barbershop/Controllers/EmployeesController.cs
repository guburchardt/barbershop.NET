using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using barbershop.Contracts.Responses;
using barbershop.Contracts.Requests;
using barbershop.Application.UseCases.Employees.CreateEmployee;
using barbershop.Domain.Entities;
using barbershop.Application.UseCases.Employees.ListEmployee;
using barbershop.Application.UseCases.Employees.GetEmployeeById;
using barbershop.Application.UseCases.Employees.UpdateEmployee;
using barbershop.Application.UseCases.Clients.DeactivateClient;
using barbershop.Application.UseCases.Employees.DeactivateEmployee;
using barbershop.Application.UseCases.Employees.ActivateEmployee;

namespace barbershop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly CreateEmployeeHandler _create;
        private readonly ListEmployeesHandler _list;
        private readonly GetEmployeeByIdHandler _getById;
        private readonly UpdateEmployeeHandler _update;
        private readonly DeactivateEmployeeHandler _deactivate;
        private readonly ActivateEmployeeHandler _activate;

        public EmployeesController(CreateEmployeeHandler create, ListEmployeesHandler list, GetEmployeeByIdHandler getById, UpdateEmployeeHandler update, DeactivateEmployeeHandler deactivate, ActivateEmployeeHandler activate)
        {
            _create = create;
            _list = list;
            _getById = getById;
            _update = update;
            _deactivate = deactivate;
            _activate = activate;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var employees = await _list.Handle(new ListEmployeesQuery(), ct);
            var response = employees.Select(e => new EmployeeResponse(e.Id, e.FullName, e.IsActive));
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var employee = await _getById.Handle(new GetEmployeeByIdQuery(id), ct);
            if (employee is null) return NotFound();

            return Ok(new EmployeeResponse(
                employee.Id,
                employee.FullName,
                employee.IsActive
            ));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request, CancellationToken ct)
        {
            var command = new CreateEmployeeCommand(request.FullName);

            var employee = await _create.Handle(command, ct);

            return Ok(new EmployeeResponse(
                employee.Id,
                employee.FullName,
                employee.IsActive
            ));
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEmployeeRequest request, CancellationToken ct)
        {
            var employee = await _update.Handle(new UpdateEmployeeCommand(id, request.FullName), ct);
            if (employee is null) return NotFound();

            return Ok(new EmployeeResponse(employee.Id, employee.FullName, employee.IsActive));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id, CancellationToken ct)
        {
            var employee = await _deactivate.Handle(new DeactivateEmployeeCommand(id), ct);
            if (employee is null) return NotFound();

            return Ok(new EmployeeResponse(employee.Id, employee.FullName, employee.IsActive));
        }

        [HttpPost("{id:guid}/activate")]
        public async Task<IActionResult> Activate([FromRoute] Guid id, CancellationToken ct)
        {
            var employee = await _activate.Handle(new ActivateEmployeeCommand(id), ct);
            if (employee is null) return NotFound();

            return Ok(new EmployeeResponse(employee.Id, employee.FullName, employee.IsActive));
        }
    }
}
