using Facturas.Models;

namespace Facturas.Repositorios.Interfaces
{
    public interface ILineaFacturaRepository
    {

        IEnumerable<LineaFactura> GetAll();
        LineaFactura GetById(int id);
        void Add(LineaFactura lineaFactura);
        void Update(LineaFactura lineaFactura);
        void Delete(int id);
        List<LineaFactura> GetLineasFacturaByFacturaId(int facturaId);

    }
}
