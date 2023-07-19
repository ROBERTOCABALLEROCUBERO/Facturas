using Facturas.Models;

namespace Facturas.Servicios.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllClientes();
        Task<Cliente> GetClienteById(int id);
        Task CreateCliente(Cliente cliente);
        Task UpdateCliente(Cliente cliente);
        Task DeleteCliente(int id);
    }
}
