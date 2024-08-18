using AutoMapper;
using CoopTest.BLL.DTOs;
using CoopTest.BLL.Repository;
using CoopTest.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoopTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FondoController : ControllerBase
    {
        private readonly IFondoRepository _fondoRepository;
        private readonly IMapper _mapper;

        public FondoController(IFondoRepository fondoRepository, IMapper mapper)
        {
            _fondoRepository = fondoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFondos()
        {
            var fondos = await _fondoRepository.GetAllAsync();
            var fondosDto = _mapper.Map<IEnumerable<FondoDTO>>(fondos);
            return Ok(fondosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFondoById(string id)
        {
            var fondo = await _fondoRepository.GetByIdAsync(id);
            if (fondo == null)
                return NotFound();

            var fondoDto = _mapper.Map<FondoDTO>(fondo);
            return Ok(fondoDto);
        }

        [HttpGet("by-name/{nombre}")]
        public async Task<IActionResult> GetFondoByNombre(string nombre)
        {
            var fondo = await _fondoRepository.GetFondoByNombreAsync(nombre);
            if (fondo == null)
                return NotFound();

            var fondoDto = _mapper.Map<FondoDTO>(fondo);
            return Ok(fondoDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFondo(FondoDTO fondoDto)
        {
            var fondo = _mapper.Map<Fondo>(fondoDto);
            await _fondoRepository.InsertAsync(fondo);
            return CreatedAtAction(nameof(GetFondoById), new { id = fondo.Id }, fondoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFondo(string id, FondoDTO fondoDto)
        {
            var fondo = _mapper.Map<Fondo>(fondoDto);
            fondo.Id = id;
            await _fondoRepository.UpdateAsync(id, fondo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFondo(string id)
        {
            await _fondoRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
