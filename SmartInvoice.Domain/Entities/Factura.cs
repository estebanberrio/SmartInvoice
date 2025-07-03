namespace SmartInvoice.Domain.Entities;

public class Factura
{
    public Guid Id { get; set; }
    public string NumeroFactura { get; set; }
    public string NitProveedor { get; set; }
    public string NombreProveedor { get; set; }
    public DateTime FechaEmision { get; set; }
    public decimal ValorTotal { get; set; }
    public string Estado { get; set; } // Pendiente, Aprobada, Rechazada, Pagada
    public string RutaArchivoPDF { get; set; }
    public string RutaArchivoXML { get; set; }
    public string? RutaComprobantePago { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
}
