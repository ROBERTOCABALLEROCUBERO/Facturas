using Facturas.DTO;
using Facturas.Models;
using Facturas.Repositorios.Interfaces;
using Facturas.Servicios.Interfaces;

namespace Facturas.Servicios.Implementaciones
{
    public class FacturaService : IFacturaService
    {

        private readonly IFacturaRepository _facturaRepository;

        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<IEnumerable<Factura>> GetAllFacturas()
        {
            return await _facturaRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Factura>> GetFacturasByClienteId(int clienteId)
        {
            return await _facturaRepository.GetByClienteIdAsync(clienteId); // Implementar en el repositorio
        }

        public async Task<Factura> GetFacturaById(int id)
        {
            return await _facturaRepository.GetByIdAsync(id);
        }

        public async Task CreateFactura(FacturasDTO factura)
        {
            await _facturaRepository.AddAsync(factura);
        }

        public async Task UpdateFactura(Factura factura)
        {
            await _facturaRepository.UpdateAsync(factura);
        }

        public async Task DeleteFactura(int id)
        {
            await _facturaRepository.DeleteAsync(id);
        
        }
        public async Task<bool> ExisteNumeroFactura(int numeroFactura)
        {
            return await _facturaRepository.ExistsByNumeroFactura(numeroFactura);
        }

    }
}
