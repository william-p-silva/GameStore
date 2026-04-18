using GameStore.Application.DTOs;
using GameStore.Application.Services;
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
                return Ok(usuario);
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

        [Authorize] [HttpGet("protegido")]
        public IActionResult Protegido()
        {
            return Ok("Acesso autorizado");
        }

    }
}
