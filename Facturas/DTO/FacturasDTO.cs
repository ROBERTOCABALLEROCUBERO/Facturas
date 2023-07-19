using Facturas.Models;

namespace Facturas.DTO
{
    public class FacturasDTO
    {
        public int NumeroFactura { get; set; }
        public int ClienteID { get; set; }
        public DateTime Fecha { get; set; }

        public decimal TotalFactura
        {
            get;
            set; // Agregar un setter vacío para evitar el error de solo lectura
        }
        public List<LineaFactura> LineasFactura { get; set; }

        public Cliente Cliente { get; set; }
    }
}
