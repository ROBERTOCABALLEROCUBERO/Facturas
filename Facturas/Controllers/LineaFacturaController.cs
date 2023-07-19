using Facturas.Models;
using Facturas.Services;
using Facturas.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Facturas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineaFacturaController : ControllerBase
    {
        private readonly ILineaFacturaService _lineaFacturaService;

        public LineaFacturaController(ILineaFacturaService lineaFacturaService)
        {
            _lineaFacturaService = lineaFacturaService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LineaFactura>> GetAllLineasFactura()
        {
            var lineasFactura = _lineaFacturaService.GetAllLineasFactura();
            return Ok(lineasFactura);
        }

        [HttpGet("{id}")]
        public ActionResult<LineaFactura> GetLineaFacturaById(int id)
        {
            var lineaFactura = _lineaFacturaService.GetLineaFacturaById(id);
            if (lineaFactura == null)
                return NotFound();

            return Ok(lineaFactura);
        }

        [HttpPost]
        public IActionResult AddLineaFactura(LineaFactura lineaFactura)
        {
            _lineaFacturaService.AddLineaFactura(lineaFactura);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLineaFactura(int id, LineaFactura lineaFactura)
        {
            var existingLineaFactura = _lineaFacturaService.GetLineaFacturaById(id);
            if (existingLineaFactura == null)
                return NotFound();

            existingLineaFactura.Concepto = lineaFactura.Concepto;
            existingLineaFactura.Unidades = lineaFactura.Unidades;
            existingLineaFactura.Precio = lineaFactura.Precio;

            _lineaFacturaService.UpdateLineaFactura(existingLineaFactura);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLineaFactura(int id)
        {
            var existingLineaFactura = _lineaFacturaService.GetLineaFacturaById(id);
            if (existingLineaFactura == null)
                return NotFound();

            _lineaFacturaService.DeleteLineaFactura(id);
            return Ok();
        }
    }
}
