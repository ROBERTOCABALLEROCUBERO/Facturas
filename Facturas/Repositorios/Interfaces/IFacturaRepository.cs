using Facturas.DTO;
using Facturas.Models;

namespace Facturas.Repositorios.Interfaces
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> GetAllAsync();
        Task<Factura> GetByIdAsync(int id);
        Task AddAsync(FacturasDTO factura);
        Task UpdateAsync(Factura factura);
        Task DeleteAsync(int id);
        Task<IEnumerable<Factura>> GetByClienteIdAsync(int clienteId);
        Task<bool> ExistsByNumeroFactura(int numeroFactura);

    }
}
