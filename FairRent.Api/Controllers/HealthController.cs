using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FairRent.Api.Controllers;

[ApiController]
[Route("health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet("live")]
    [AllowAnonymous]
    public IActionResult Live() => Ok(new { status = "ok" });

    [HttpGet("secure")]
    [Authorize]
    public IActionResult Secure() => Ok(new { status = "secure-ok", user = User.Identity?.Name });
}
