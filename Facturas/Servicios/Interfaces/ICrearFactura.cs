using Facturas.DTO;
using Facturas.Models;

namespace Facturas.Servicios.Interfaces
{
    public interface ICrearFactura
    {
        void PrintFactura(FacturasDTO factura);
    }
}
