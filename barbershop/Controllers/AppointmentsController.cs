using barbershop.Application.UseCases.Appointments.CancelAppointment;
using barbershop.Application.UseCases.Appointments.CreateAppointment;
using barbershop.Application.UseCases.Appointments.GetAppointmentById;
using barbershop.Contracts.Requests;
using barbershop.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace barbershop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly CreateAppointmentHandler _create;
        private readonly CancelAppointmentHandler _cancel;
        private readonly GetAppointmentByIdHandler _getById;

        public AppointmentsController(CreateAppointmentHandler create, CancelAppointmentHandler cancel, GetAppointmentByIdHandler getById)
        {
            _create = create;
            _cancel = cancel;
            _getById = getById;
        }

        [HttpGet("{id:guid}")]
        public async Task <IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var appointment = await _getById.Handle(new GetAppointmentByIdQuery(id), ct);
            if (appointment is null) return NotFound();

            return Ok(new AppointmentResponse(
                appointment.Id,
                appointment.EmployeeId,
                appointment.ClientId,
                appointment.StartAt,
                appointment.EndAt,
                appointment.Status.ToString(),
                appointment.CancelledAt,
                appointment.CancelReason
            ));
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] CreateAppointmentRequest request, CancellationToken ct)
        {
            var command = new CreateAppointmentCommand(
                request.EmployeeId,
                request.ClientId,
                request.StartAt,
                request.EndAt
            );

            var appointment = await _create.Handle(command, ct);

            return Ok(new AppointmentResponse(
                appointment.Id,
                appointment.EmployeeId,
                appointment.ClientId,
                appointment.StartAt,
                appointment.EndAt,
                appointment.Status.ToString(),
                appointment.CancelledAt,
                appointment.CancelReason
            ));
        }

        [HttpPost("{id:guid}/cancel")]
        public async Task <IActionResult> Cancel([FromRoute] Guid id, [FromBody] CancelAppointmentRequest request, CancellationToken ct)
        {
            var appointment = await _cancel.Handle(new CancelAppointmentCommand(id, request.CancelReason), ct);
            if (appointment is null) return NotFound();

            return Ok(new AppointmentResponse(
                appointment.Id,
                appointment.EmployeeId,
                appointment.ClientId,
                appointment.StartAt,
                appointment.EndAt,
                appointment.Status.ToString(),
                appointment.CancelledAt,
                appointment.CancelReason
            ));
        }

    }
}
