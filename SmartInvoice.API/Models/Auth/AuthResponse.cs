namespace SmartInvoice.API.Models.Auth;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? NombreCompleto { get; set; }
}
