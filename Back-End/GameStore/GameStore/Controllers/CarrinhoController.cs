using GameStore.Application.DTOs.Carrinho;
using GameStore.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly CarrinhoService _service;
        public CarrinhoController(CarrinhoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("carrinho")]
        public async Task<IActionResult> ObterCarrinho()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var carrinho = await _service.ObterOuCriarCarrinho(userId);
            return Ok(carrinho);
        }

        [Authorize]
        [HttpPost("adicionarItem")]
        public async Task<IActionResult> AdicionatItemCarrinho(CarrinhoAdcionarItemDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _service.AdicionarProduto(dto, userId);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("removerItem")]
        public async Task<IActionResult> RemoverItemCarrinho(CarrinhoRemoverItemDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _service.RemoverProduto(dto, userId);
            return NoContent();
        }
    }
}
