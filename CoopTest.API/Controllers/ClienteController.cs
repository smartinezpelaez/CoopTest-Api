using AutoMapper;
using CoopTest.BLL.DTOs;
using CoopTest.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoopTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpPost("crear-cliente")]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                // Crear o actualizar el cliente
                await _clienteService.CrearOActualizarClienteAsync(clienteDTO);

                return Ok(new { mensaje = "Cliente creado o actualizado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpPost("suscribir-cliente")]
        public async Task<IActionResult> SuscribirCliente([FromBody] SuscripcionFondoDTO suscripcionFondoDTO)
        {
            try
            {
                // Suscribir el cliente al fondo
                await _clienteService.SuscribirClienteAFondoAsync(suscripcionFondoDTO);

                return Ok(new { mensaje = "Suscripción realizada con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }




        [HttpPost("{clienteId}/fondos/{fondoId}/cancelar")]
        public async Task<IActionResult> CancelarSuscripcion(string clienteId, string fondoId)
        {
            try
            {
                await _clienteService.CancelarSuscripcionAsync(clienteId, fondoId);
                return Ok(new { mensaje = "Cancelación realizada con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }

}
