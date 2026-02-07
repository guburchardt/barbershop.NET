using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using barbershop.Contracts.Responses;
using barbershop.Contracts.Requests;

namespace barbershop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
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
        public IActionResult Create([FromBody] CreateEmployeeRequest request)
        {
            var createdEmployee = new EmployeeResponse(
                Guid.NewGuid(),
                request.FullName,
                true
            );

            // Return Created (201) + Location header + body
            return CreatedAtAction(
              nameof(GetById),
              new { id = createdEmployee.Id},
              createdEmployee  
            );
        }
    }
}
