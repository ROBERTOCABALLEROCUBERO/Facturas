namespace Facturas.Models
{
    public class LineaFactura
    {
 
            public int LineaFacturaId { get; set; }
            public int FacturaId { get; set; }
            public string Concepto { get; set; }
            public int Unidades { get; set; }
            public decimal Precio { get; set; }
        public Factura? Factura { get; set; }
        public decimal Importe => Unidades * Precio;
     
        }
    }

