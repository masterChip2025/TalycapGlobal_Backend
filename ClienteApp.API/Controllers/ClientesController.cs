using System;
using ClienteApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService)
		{
            _clienteService = clienteService;
        }

        [HttpGet("{identificacion}")]
        public async Task<IActionResult> Get(string identificacion)
        {
            var cliente = await _clienteService.ObtenerClienteAsync(identificacion);

            if (cliente == null)
                return NotFound(new { mensaje = "El cliente solicitado no existe." });

            return Ok(cliente);
        }
    }
}

