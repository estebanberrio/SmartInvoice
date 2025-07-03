using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartInvoice.Infrastructure.Identity;

namespace SmartInvoice.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // Solo admins pueden gestionar usuarios
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // GET: api/users
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userManager.Users.Select(u => new
        {
            u.Id,
            u.Email,
            u.NombreCompleto
        }).ToList();

        return Ok(users);
    }

    // GET: api/users/{userId}/roles
    [HttpGet("{userId}/roles")]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("Usuario no encontrado.");

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(roles);
    }

    // POST: api/users/{userId}/roles/{role}
    [HttpPost("{userId}/roles/{role}")]
    public async Task<IActionResult> AddUserToRole(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("Usuario no encontrado.");

        var result = await _userManager.AddToRoleAsync(user, role);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok($"Rol '{role}' asignado a {user.Email}.");
    }

    // DELETE: api/users/{userId}/roles/{role}
    [HttpDelete("{userId}/roles/{role}")]
    public async Task<IActionResult> RemoveUserFromRole(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("Usuario no encontrado.");

        var result = await _userManager.RemoveFromRoleAsync(user, role);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok($"Rol '{role}' removido de {user.Email}.");
    }
}
