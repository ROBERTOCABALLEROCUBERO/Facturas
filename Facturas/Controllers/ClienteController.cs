using Facturas.Models;
using Facturas.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Facturas.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            return await _clienteService.GetAllClientes();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id)
        {
            var cliente = await _clienteService.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente(Cliente cliente)
        {
            await _clienteService.CreateCliente(cliente);
            return CreatedAtAction(nameof(GetClienteById), new { id = cliente.ID }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, Cliente cliente)
        {
            if (id != cliente.ID)
            {
                return BadRequest();
            }

            await _clienteService.UpdateCliente(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await _clienteService.DeleteCliente(id);
            return NoContent();
        }
    }
}