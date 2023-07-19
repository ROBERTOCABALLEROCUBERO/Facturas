using Facturas.DTO;
using Facturas.Models;
using Facturas.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Facturas.Controllers
{
    [ApiController]
    [Route("api/facturas")]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;
        private readonly IClienteService _clienteService;
        private readonly ILineaFacturaService _lineaFacturaService;
        private readonly ICrearFactura _crearFactura;
        public FacturaController(IFacturaService facturaService, IClienteService clienteService, ILineaFacturaService lineaFacturaService, ICrearFactura crearFactura)
        {
            _facturaService = facturaService;
            _clienteService = clienteService;
            _lineaFacturaService = lineaFacturaService;
            _crearFactura = crearFactura;
        }

        [HttpGet]
        public async Task<IEnumerable<Factura>> GetAllFacturas()
        {
            return await _facturaService.GetAllFacturas();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFacturaById(int id)
        {
            var factura = await _facturaService.GetFacturaById(id);
            if (factura == null)
            {
                return NotFound();
            }
            return factura;
        }

        [HttpPost("add")]
        public async Task<IActionResult> addfactura(int clienteId, int num) {

            FacturasDTO facturaDto = new FacturasDTO
            {
                Fecha = DateTime.Now,
                NumeroFactura = num,
                ClienteID = clienteId

            };
           await _facturaService.CreateFactura(facturaDto);
            return Ok(facturaDto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearFactura(int clienteId, int num)
        {
            // Consulta para obtener los datos del cliente
           var cliente = await _clienteService.GetClienteById(clienteId);

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            // Crear nueva factura
            FacturasDTO factura = new FacturasDTO
            {
                NumeroFactura = num,
                ClienteID = clienteId,
                Fecha = DateTime.Now,
                LineasFactura = new List<LineaFactura>(),
                Cliente = cliente
            };


            // Consulta para obtener las líneas de factura del cliente
            List<LineaFactura> lineasFactura = _lineaFacturaService.GetLineasFacturaByFacturaId(factura.NumeroFactura);


            // Agregar las líneas de factura a la factura
            factura.LineasFactura.AddRange(lineasFactura);

            // Calcular el total de la factura
            factura.TotalFactura = lineasFactura.Sum(lf => lf.Importe);

            // Guardar la factura en la base de datos
          

            _crearFactura.PrintFactura(factura);

            return Ok(factura);
        }

        [HttpGet("Descargar")]
        public IActionResult DescargarFactura(string numeroFactura)
        {
            // Aquí debes agregar la lógica para obtener la ruta del archivo generado en tu servidor
            string rutaArchivo = ObtenerRutaArchivo(numeroFactura);

            if (!string.IsNullOrEmpty(rutaArchivo) && System.IO.File.Exists(rutaArchivo))
            {
                // Lee el archivo como un arreglo de bytes
                byte[] archivoBytes = System.IO.File.ReadAllBytes(rutaArchivo);

                // Establece el tipo MIME del archivo
                string tipoMime = "application/pdf"; // Aquí debes ajustarlo al tipo de archivo correcto

                // Devuelve el archivo como resultado para descarga
                return File(archivoBytes, tipoMime, "factura.pdf"); // Aquí puedes especificar el nombre del archivo deseado para la descarga
            }
            else
            {
                // Manejo del caso en el que el archivo no existe
                // Por ejemplo, puedes redirigir a una página de error
                return NotFound();
            }
        }

        private string ObtenerRutaArchivo(string numeroFactura)
        {
            // Aquí debes implementar la lógica para obtener la ruta del archivo generado en tu servidor
            // Puedes utilizar el número de factura para construir la ruta del archivo o buscarlo en una ubicación específica
            // Retorna la ruta completa del archivo generado
            string rutaArchivo = "ruta/del/archivo.pdf";
            return rutaArchivo;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFactura(int id, Factura factura)
        {
            if (id != factura.NumeroFactura)
            {
                return BadRequest();
            }

            await _facturaService.UpdateFactura(factura);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            await _facturaService.DeleteFactura(id);
            return NoContent();
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturasByClienteId(int clienteId)
        {
            var facturas = await _facturaService.GetFacturasByClienteId(clienteId);
            return Ok(facturas);
        }

        [HttpGet("cliente/numero")]
        public async Task<int> GenerarNumeroFacturaUnico()
        {
            Random random = new Random();
            int numeroFactura;

            // Generar un número de factura aleatorio y verificar su unicidad en la base de datos
            while (true)
            {
                numeroFactura = random.Next(1, 1000000); // Generar un número aleatorio entre 1 y 999999

                // Verificar si el número de factura ya existe en la base de datos
                bool existeNumeroFactura = await _facturaService.ExisteNumeroFactura(numeroFactura);

                if (!existeNumeroFactura)
                {
                    // El número de factura es único, salir del bucle
                    break;
                }
            }

            return numeroFactura;
        }


    }
}