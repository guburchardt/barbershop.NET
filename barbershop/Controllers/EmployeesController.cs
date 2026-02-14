using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using barbershop.Contracts.Responses;
using barbershop.Contracts.Requests;
using barbershop.Application.UseCases.Employees.CreateEmployee;
using barbershop.Domain.Entities;

namespace barbershop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly CreateEmployeeHandler _create;

        public EmployeesController(CreateEmployeeHandler create)
        {
            _create = create;
        }

        // GET ALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = new List <EmployeeResponse>
            {
                new EmployeeResponse(Guid.NewGuid(), "Barbeiro 1", true),
                new EmployeeResponse(Guid.NewGuid(), "Barbeiro 2", true),
            };

            return Ok(employees);
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
