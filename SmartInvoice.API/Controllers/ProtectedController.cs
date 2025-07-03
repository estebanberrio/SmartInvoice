using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartInvoice.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // ⛔ Requiere JWT válido
public class ProtectedController : ControllerBase
{
    [HttpGet("secret")]
    public IActionResult GetSecret()
    {
        var userEmail = User.Identity?.Name;
        return Ok(new
        {
            message = "Este es un endpoint protegido.",
            usuario = userEmail
        });
    }
}
