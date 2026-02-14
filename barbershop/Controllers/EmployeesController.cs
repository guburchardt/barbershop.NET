using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using barbershop.Contracts.Responses;
using barbershop.Contracts.Requests;
using barbershop.Application.UseCases.Employees.CreateEmployee;
using barbershop.Domain.Entities;
using barbershop.Application.UseCases.Employees.ListEmployee;

namespace barbershop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly CreateEmployeeHandler _create;
        private readonly ListEmployeesHandler _list;

        public EmployeesController(CreateEmployeeHandler create, ListEmployeesHandler list)
        {
            _create = create;
            _list = list;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var employees = await _list.Handle(new ListEmployeesQuery(), ct);
            var response = employees.Select(e => new EmployeeResponse(e.Id, e.FullName, e.IsActive));
            return Ok(response);
        }

        // GET BY id
        [HttpGet("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Fake
            var employee = new EmployeeResponse(id, "Barbeiro Exemplo", true);
            return Ok(employee);
        }

        // POST
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
    }
}
