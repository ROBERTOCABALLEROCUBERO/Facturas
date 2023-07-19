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
        private const string DocumentPath = "DOCS/Actuales";
        private const string DocumentPathPasados = "./DOCS/PASADOS/";// Ruta relativa a la carpeta de documentos

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

        [HttpPut("{numfactura}")]
        public async Task<IActionResult> UpdateFactura(int numfactura, int id)
        {
            Factura factura = await _facturaService.GetFacturaById(numfactura);

            if (factura == null)
            {
                return BadRequest();
            }

            string nombreArchivo = $"Num{numfactura}.pdf";
            string rutaCarpeta = Path.Combine(DocumentPath);

            // Buscar todos los archivos PDF que coincidan con el patrón
            string[] archivos = Directory.GetFiles(rutaCarpeta, $"*{nombreArchivo}");

            // Verificar si el archivo existe
            if (!archivos.Any())
            {
                return NotFound("Factura no encontrada");
            }

            string archivoExistente = archivos.First();

            // Crear la ruta de destino
            string rutaCarpetaPasados = Path.Combine(DocumentPathPasados);
            string nombreArchivoPasado = $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_{Path.GetFileName(archivoExistente)}";
            string rutaArchivoPasado = Path.Combine(rutaCarpetaPasados, nombreArchivoPasado);

            // Mover el archivo a la carpeta de pasados con el nuevo nombre
            System.IO.File.Move(archivoExistente, rutaArchivoPasado);

           await CrearFactura(id, numfactura);

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

        [HttpGet("archivo")]
        public async Task<IActionResult> GetFacturaArchivo(int identificador)
        {
            Factura factura = await _facturaService.GetFacturaById(identificador);

            if (factura == null)
            {
                return BadRequest("Identificador inválido");
            }

            string nombreArchivo = $"Num{identificador}.pdf";
            string rutaCarpeta = Path.Combine(DocumentPath);

            // Buscar todos los archivos PDF que coincidan con el patrón
            string[] archivos = Directory.GetFiles(rutaCarpeta, $"*{nombreArchivo}");

            if (archivos.Length > 0)
            {
                // Tomar el primer archivo encontrado
                string rutaArchivo = archivos.First();
                byte[] archivoBytes = System.IO.File.ReadAllBytes(rutaArchivo);
                return File(archivoBytes, "application/pdf", "factura");
            }

            return NotFound("Factura no encontrada");
        }

    }
}