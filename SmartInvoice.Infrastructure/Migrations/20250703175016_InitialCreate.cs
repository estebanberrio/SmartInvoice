using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartInvoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroFactura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NitProveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreProveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RutaArchivoPDF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RutaArchivoXML = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RutaComprobantePago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturas");
        }
    }
}
