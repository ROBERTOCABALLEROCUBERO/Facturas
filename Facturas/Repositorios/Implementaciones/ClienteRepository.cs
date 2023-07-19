using Facturas.Mapper;
using Facturas.Models;
using Facturas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Facturas.Repositorios.Implementaciones
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly FacturasContext _context;

        public ClienteRepository(FacturasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }
    
        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
