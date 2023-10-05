using Microsoft.AspNetCore.Mvc;

namespace ShiftLoggerApi.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
internal class PingController : ControllerBase
{
    // GET: api/Ping
    [HttpGet]
    internal ActionResult<string> Get()
    {
        return Ok("Pong");
    }
}