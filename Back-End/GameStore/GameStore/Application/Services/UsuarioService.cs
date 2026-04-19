using GameStore.Application.DTOs;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<Usuario> CriarUsuario(UsuarioCreateDto dto)
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
            return usuario;
        }

        public async Task<string> Login(LoginUsuarioDto dto, IConfiguration config)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email.Trim().ToLower());

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
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Role)
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

        public async Task<List<Usuario>> Listar()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> BuscarId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
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
