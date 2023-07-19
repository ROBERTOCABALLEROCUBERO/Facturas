using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facturas.Models
{
    public class Factura
    {
        public int NumeroFactura { get; set; }
        public int ClienteID { get; set; }
        public DateTime Fecha { get; set; }
        public List<LineaFactura> LineasFactura { get; set; }

        public Cliente Cliente { get; set; }


    }
}
