using Facturas.Models;
using Facturas.Repositories;
using Facturas.Repositorios.Interfaces;
using Facturas.Servicios.Interfaces;
using System.Collections.Generic;

namespace Facturas.Services
{
    public class LineaFacturaService : ILineaFacturaService
    {
        private readonly ILineaFacturaRepository _lineaFacturaRepository;

        public LineaFacturaService(ILineaFacturaRepository lineaFacturaRepository)
        {
            _lineaFacturaRepository = lineaFacturaRepository;
        }

        public IEnumerable<LineaFactura> GetAllLineasFactura()
        {
            return _lineaFacturaRepository.GetAll();
        }

        public LineaFactura GetLineaFacturaById(int id)
        {
            return _lineaFacturaRepository.GetById(id);
        }

        public void AddLineaFactura(LineaFactura lineaFactura)
        {
            _lineaFacturaRepository.Add(lineaFactura);
        }

        public void UpdateLineaFactura(LineaFactura lineaFactura)
        {
            _lineaFacturaRepository.Update(lineaFactura);
        }

        public void DeleteLineaFactura(int id)
        {
            _lineaFacturaRepository.Delete(id);
        }

        public List<LineaFactura> GetLineasFacturaByFacturaId(int facturaId)
        {
            return _lineaFacturaRepository.GetLineasFacturaByFacturaId(facturaId);
    }
    }
}
