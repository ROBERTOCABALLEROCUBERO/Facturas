using Facturas.Models;
using Facturas.Repositorios.Interfaces;
using Facturas.Servicios.Interfaces;

namespace Facturas.Servicios.Implementaciones
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        public async Task CreateCliente(Cliente cliente)
        {
            await _clienteRepository.AddAsync(cliente);
        }

        public async Task UpdateCliente(Cliente cliente)
        {
            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task DeleteCliente(int id)
        {
            await _clienteRepository.DeleteAsync(id);
        }
    }
}
