using AutoMapper;
using CoopTest.BLL.DTOs;
using CoopTest.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoopTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;
        private readonly IMapper _mapper;

        public TransaccionController(ITransaccionService transaccionService, IMapper mapper)
        {
            _transaccionService = transaccionService;
            _mapper = mapper;
        }

        [HttpGet("{clienteId}")]
        public async Task<IActionResult> VerHistorialTransacciones(string clienteId)
        {
            var transacciones = await _transaccionService.VerHistorialTransaccionesAsync(clienteId);
            var transaccionesDTO = _mapper.Map<IEnumerable<TransaccionDTO>>(transacciones);
            return Ok(transaccionesDTO);
        }
    }
}
