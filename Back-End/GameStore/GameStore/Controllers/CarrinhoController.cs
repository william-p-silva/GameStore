using GameStore.Application.DTOs;
using GameStore.Application.DTOs.Carrinho;
using GameStore.Application.DTOs.Produto;
using GameStore.Application.Services;
using GameStore.Domain.Entities;
using GameStore.Helpers;
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
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var carrinho = await _service.ObterOuCriarCarrinho(userId);
                return Ok(ApiResponse<CarrinhoResponseDto>.Ok(carrinho));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize]
        [HttpPost("adicionarItem")]
        public async Task<IActionResult> AdicionatItemCarrinho(CarrinhoAdcionarItemDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var carrinho = await _service.AdicionarProduto(dto, userId);
                return Ok(ApiResponse<CarrinhoResponseDto>.Ok(carrinho));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize]
        [HttpDelete("removerItem")]
        public async Task<IActionResult> RemoverItemCarrinho(CarrinhoRemoverItemDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var carrinho = await _service.RemoverProduto(dto, userId);
                return Ok(ApiResponse<CarrinhoResponseDto>.Ok(carrinho));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize]
        [HttpPut("atualizarItem")]
        public async Task<IActionResult> AtualizarItemCarrinho(CarrinhoAtualizarItemDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var carrinho = await _service.AtualizarItemCarrinho(dto, userId);
                return Ok(ApiResponse<CarrinhoResponseDto>.Ok(carrinho));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
