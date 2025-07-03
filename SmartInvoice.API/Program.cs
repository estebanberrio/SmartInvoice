using Microsoft.EntityFrameworkCore;
using SmartInvoice.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SmartInvoice.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// 🔧 Agregar servicios de DbContext y SQL Server
builder.Services.AddDbContext<SmartInvoiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔐 Configurar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<SmartInvoiceDbContext>()
    .AddDefaultTokenProviders();

// 🔑 Configuración JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
    };
});

// 🧩 Agregar soporte para controladores
builder.Services.AddControllers();

// 🌐 Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartInvoice API", Version = "v1" });
});

var app = builder.Build();

// 🌐 Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de autenticación y autorización
//app.UseHttpsRedirection();
app.UseAuthentication(); // ✅ IMPORTANTE: va antes de UseAuthorization
app.UseAuthorization();

// 🚀 Mapear controladores
app.MapControllers();


//Código para crear roles y un usuario administrador por defecto
async Task SeedRolesAndAdminUserAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = ["Admin", "User"];

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    // Crear usuario admin por defecto si no existe
    var adminEmail = "admin@smartinvoice.local";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            NombreCompleto = "Administrador General"
        };

        var result = await userManager.CreateAsync(newAdmin, "Admin123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}

await SeedRolesAndAdminUserAsync(app);

app.Run();
