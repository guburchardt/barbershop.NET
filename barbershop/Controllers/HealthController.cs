using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace barbershop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new {status = "ok"});
        }
    }
}
