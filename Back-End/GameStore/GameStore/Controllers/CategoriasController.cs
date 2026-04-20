using GameStore.Application.DTOs.Categoria;
using GameStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaService _service;
        public CategoriasController(CategoriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CategoriaCreateDto dto)
        {
            try
            {
                var categoria = await _service.Criar(dto);
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] CategoriaFiltroDto filtro)
        {
            try
            {
                var categorias = await _service.Listar(filtro);
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                var categoria = await _service.BuscarId(id);
                if (categoria == null)
                    return NotFound();
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, CategoriaUpdateDto dto)
        {
            try
            {
                await _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _service.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
