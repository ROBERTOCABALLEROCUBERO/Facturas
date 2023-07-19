using Facturas.DTO;
using Facturas.Models;

namespace Facturas.Servicios.Interfaces
{
    public interface IFacturaService
    {
        Task<IEnumerable<Factura>> GetAllFacturas();
        Task<Factura> GetFacturaById(int id);
        Task CreateFactura(FacturasDTO factura);
        Task UpdateFactura(Factura factura);
        Task DeleteFactura(int id);
        Task<bool> ExisteNumeroFactura(int numeroFactura);
        Task<IEnumerable<Factura>> GetFacturasByClienteId(int clienteId);
    }
}
