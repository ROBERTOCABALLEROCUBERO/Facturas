using Facturas.Mapper;
using Facturas.Models;
using Facturas.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Facturas.Repositories
{
    public class LineaFacturaRepository : ILineaFacturaRepository
    {
        private readonly FacturasContext _context;

        public LineaFacturaRepository(FacturasContext context)
        {
            _context = context;
        }

        public IEnumerable<LineaFactura> GetAll()
        {
            return _context.LineasFacturas.ToList();
        }

        public LineaFactura GetById(int id)
        {
            return _context.LineasFacturas.Find(id);
        }
        public List<LineaFactura> GetLineasFacturaByFacturaId(int facturaId)
        {
            return _context.LineasFacturas
                .Where(lf => lf.FacturaId == facturaId)
                .ToList();
        }
        public void Add(LineaFactura lineaFactura)
        {
            _context.LineasFacturas.Add(lineaFactura);
            _context.SaveChanges();
        }

        public void Update(LineaFactura lineaFactura)
        {
            _context.LineasFacturas.Update(lineaFactura);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var lineaFactura = _context.LineasFacturas.Find(id);
            if (lineaFactura != null)
            {
                _context.LineasFacturas.Remove(lineaFactura);
                _context.SaveChanges();
            }
        }
    }
}

