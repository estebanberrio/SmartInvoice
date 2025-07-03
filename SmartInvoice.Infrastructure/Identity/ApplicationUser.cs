using Microsoft.AspNetCore.Identity;

namespace SmartInvoice.Infrastructure.Identity;


public class ApplicationUser : IdentityUser
{
    // Puedes extender con más propiedades si deseas
    public string? NombreCompleto { get; set; }
}
