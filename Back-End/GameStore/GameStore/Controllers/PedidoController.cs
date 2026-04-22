using GameStore.Application.DTOs;
using GameStore.Application.DTOs.Pedido;
using GameStore.Application.Services;
using GameStore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _service;
        public PedidoController(PedidoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CriarPedido()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var pedido = await _service.CriarPedido(userId);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] PedidofiltroDto filtro)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var pedidos = await _service.Listar(filtro, userId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> BuscarId(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var pedido = await _service.BuscarId(id, userId);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPut("cancelar/{id:int}")]
        public async Task<IActionResult> CancelarPedido(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _service.CancelarPedido(id, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("atualizarStatus/{id:int}")]
        public async Task<IActionResult> AtualizarStatusPedido(int id, AtualizarStatusPedidoDto dto)
        {
            try
            {
                await _service.AtualizarStatus( id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
