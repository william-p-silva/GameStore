using GameStore.Application.DTOs.Produto;
using GameStore.Application.Services;
using GameStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _service;
        public ProdutosController(ProdutoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(ProdutoCreateDto dto)
        {
            try
            {
                var produto = await _service.Criar(dto);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, ProdutoUpdateDto dto)
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


        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ProdutoFiltroDto filtro)
        {
            try
            {
                var produtos = await _service.Listar(filtro);
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> BuscarId(int id)
        {
            try
            {
                var produto = await _service.BuscarId(id);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remover(int id)
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
