using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SmartInvoice.Infrastructure.Identity;
using SmartInvoice.API.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace SmartInvoice.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            NombreCompleto = request.NombreCompleto
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        await _userManager.AddToRoleAsync(user, "User");
    
        if (result.Errors.Any(e => e.Code == "DuplicateUserName"))
        {
            return BadRequest("El nombre de usuario ya está en uso.");
        }


        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { message = "Usuario registrado correctamente." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Unauthorized("Usuario no encontrado.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return Unauthorized("Contraseña incorrecta.");

        var token = await GenerateJwtToken(user);


        return Ok(new AuthResponse
        {
            Token = token,
            Email = user.Email!,
            NombreCompleto = user.NombreCompleto
        });
    }

    private async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("uid", user.Id),
            new Claim(ClaimTypes.Name, user.UserName!)
        };

        // Agregar roles como claims
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
