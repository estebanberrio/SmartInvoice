using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartInvoice.Infrastructure.Identity;
using SmartInvoice.Domain.Entities;

namespace SmartInvoice.Infrastructure.Persistence;

public class SmartInvoiceDbContext : IdentityDbContext<ApplicationUser>
{
    public SmartInvoiceDbContext(DbContextOptions<SmartInvoiceDbContext> options) : base(options) { }

    public DbSet<Factura> Facturas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // ⚠️ Importante: base primero

        // Evita truncamientos en SQL Server
        modelBuilder.Entity<Factura>()
            .Property(f => f.ValorTotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Factura>().HasKey(f => f.Id);
    }
}
