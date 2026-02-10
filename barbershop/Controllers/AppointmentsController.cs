using barbershop.Application.UseCases.Appointments.CreateAppointment;
using barbershop.Contracts.Requests;
using barbershop.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace barbershop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly CreateAppointmentHandler _handler;

        public AppointmentsController(CreateAppointmentHandler handler)
        {
            _handler = handler;
        }

        //POST

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] CreateAppointmentRequest request, CancellationToken ct)
        {
            var command = new CreateAppointmentCommand(
                request.EmployeeId,
                request.ClientId,
                request.StartAt,
                request.EndAt
            );

            var appointment = await _handler.Handle(command, ct);

            return Ok(new AppointmentResponse(
                appointment.Id,
                appointment.EmployeeId,
                appointment.ClientId,
                appointment.StartAt,
                appointment.EndAt,
                appointment.Status.ToString()
            ));
        }

    }
}
