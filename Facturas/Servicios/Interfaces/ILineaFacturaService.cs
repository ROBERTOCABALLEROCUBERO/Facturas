using Facturas.Models;

namespace Facturas.Servicios.Interfaces
{
    public interface ILineaFacturaService
    {
        IEnumerable<LineaFactura> GetAllLineasFactura();
        LineaFactura GetLineaFacturaById(int id);
        void AddLineaFactura(LineaFactura lineaFactura);
        void UpdateLineaFactura(LineaFactura lineaFactura);
        void DeleteLineaFactura(int id);

        List<LineaFactura> GetLineasFacturaByFacturaId(int facturaId);
    }
}
