using BCrypt.Net;
using GameStore.Application.DTOs;
using GameStore.Application.DTOs.Usuario;
using GameStore.Domain.Entities;
using GameStore.Helpers;
using GameStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace GameStore.Application.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<UsuarioResponseDto> CriarUsuario(UsuarioCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("O email é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Senha))
                throw new ArgumentException("A senha é obrigatória.");

            var existe = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email.ToLower().Trim());
            if (existe)
                throw new ArgumentException("Já existe um usuário com este email.");

            var usuario = new Usuario
            {
                Nome = dto.Nome.Trim(),
                Email = dto.Email.ToLower().Trim(),
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return  new UsuarioResponseDto
            {
                Nome = usuario.Nome,
                Role = usuario.Role,
                Email = usuario.Email.ToLower().Trim(),
                Id = usuario.Id,
            };
        }

        public async Task<UsuarioResponseDto> CriarUsuarioAdmin(UsuarioCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("O email é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Senha))
                throw new ArgumentException("A senha é obrigatória.");

            var existe = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email.ToLower().Trim());
            if (existe)
                throw new ArgumentException("Já existe um usuário com este email.");

            var usuario = new Usuario
            {
                Nome = dto.Nome.Trim(),
                Email = dto.Email.ToLower().Trim(),
                Role = Roles.Admin,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioResponseDto
            {
                Nome = usuario.Nome,
                Role = usuario.Role,
                Email = usuario.Email.ToLower().Trim(),
                Id = usuario.Id,
            };
        }

        public async Task<string> Login(LoginUsuarioDto dto, IConfiguration config)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null)
                throw new ArgumentException("usuário ou senha inválido");

            var senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash);

            if (!senhaValida)
                throw new ArgumentException("usuário ou senha inválido");

            return GerarToken(usuario, config);
        }



        public string GerarToken(Usuario usuario, IConfiguration config)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim("email", usuario.Email),
                new Claim("role", usuario.Role),
                new Claim("nome", usuario.Nome)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task Remover(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
                throw new ArgumentException("Usuário inexistente");
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();

        }

        public async Task<PageResultDto<UsuarioResponseDto>> Listar(UsuarioFiltroDto filtro)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
                query = query.Where(e => e.Nome.Contains(filtro.Nome));
            if (!string.IsNullOrWhiteSpace(filtro.Email))
                query = query.Where(p => p.Email.Contains(filtro.Email));

            filtro.Normalizar();

            return await query.ToPagedResultAsync(
                filtro.Page,
                filtro.PageSize,
                user => new UsuarioResponseDto
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Email = user.Email,
                    Role = user.Role
                    
                });
        }

        public async Task<UsuarioResponseDto?> BuscarId(int id)
        {
            return await _context.Usuarios.Select(user => new UsuarioResponseDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
            }).FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task Atualizar(int id, UsuarioUpdateDto dto)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
                throw new ArgumentException("Usuário não encontrado");

            user.Nome = dto.Nome;
            user.Email = dto.Email;
            user.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);
            await _context.SaveChangesAsync();
        }
    }
}
