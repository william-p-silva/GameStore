using GameStore.Application.DTOs;
using GameStore.Application.DTOs.Pedido;
using GameStore.Application.DTOs.Usuario;
using GameStore.Application.Services;
using GameStore.Domain.Entities;
using GameStore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;
        private readonly IConfiguration _configuration;
        public UsuariosController(UsuarioService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }
        

        [HttpPost]
        public async Task<IActionResult> Criar(UsuarioCreateDto dto)
        {
            try
            {
                var usuario = await _service.CriarUsuario(dto);
                return Ok(ApiResponse<UsuarioResponseDto>.Ok(usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("criarAdmin")]
        public async Task<IActionResult> CriarAdmin(UsuarioCreateDto dto)
        {
            try
            {
                var usuario = await _service.CriarUsuarioAdmin(dto);
                return Ok(ApiResponse<UsuarioResponseDto>.Ok(usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            try
            {
                var token = await _service.Login(dto, _configuration);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] UsuarioFiltroDto filtro)
        {
            try
            {
                var usuarios = await _service.Listar(filtro);
                return Ok(ApiResponse<PageResultDto<UsuarioResponseDto>>.Ok(usuarios));
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
                var usuario = await _service.BuscarId(id);
                return Ok(ApiResponse<UsuarioResponseDto>.Ok(usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize]
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

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, UsuarioUpdateDto dto)
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
    }
}
