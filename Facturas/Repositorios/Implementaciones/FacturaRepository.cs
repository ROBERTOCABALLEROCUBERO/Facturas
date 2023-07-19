using Facturas.DTO;
using Facturas.Mapper;
using Facturas.Models;
using Facturas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Facturas.Repositorios.Implementaciones
{
    public class FacturaRepository : IFacturaRepository
    {

      
            private readonly FacturasContext _context;

            public FacturaRepository(FacturasContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Factura>> GetAllAsync()
            {

                return await _context.Facturas.ToListAsync();
            }

            public async Task<Factura> GetByIdAsync(int id)
            {
                return await _context.Facturas.FindAsync(id);
            }

            public async Task AddAsync(FacturasDTO factura)
            {
            Factura facturanueva = new Factura
            {
                NumeroFactura = factura.NumeroFactura,
                ClienteID = factura.ClienteID,
                Fecha = factura.Fecha
            };
                await _context.Facturas.AddAsync(facturanueva);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Factura factura)
            {
                _context.Entry(factura).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var factura = await _context.Facturas.FindAsync(id);
                if (factura != null)
                {
                    _context.Facturas.Remove(factura);
                    await _context.SaveChangesAsync();
                }
            }
        public async Task<IEnumerable<Factura>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.Facturas
                .Where(f => f.ClienteID == clienteId)
                .ToListAsync();
        }

        public async Task<bool> ExistsByNumeroFactura(int numeroFactura)
        {
            return await _context.Facturas.AnyAsync(f => f.NumeroFactura == numeroFactura);
        }
    }


    }

