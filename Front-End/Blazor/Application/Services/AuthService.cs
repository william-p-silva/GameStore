using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components;
using Blazor.Application.DTOs.Usuario;

namespace Blazor.Application.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly TokenService _tokenSvc;
        private readonly NavigationManager _navigationManager; // Adicione este campo

        // Adicione o NavigationManager no construtor
        public AuthService(IHttpClientFactory clientFactory, TokenService tokenSvc, NavigationManager navigationManager)
        {
            _http = clientFactory.CreateClient("Publico");
            _tokenSvc = tokenSvc;
            _navigationManager = navigationManager;
        }
        public async Task Login(LoginDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Usuarios/login", dto);

            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(resultado.Token);

                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

                await _tokenSvc.SalvarToken(resultado.Token);

                switch (role)
                {
                    // Ajuste os nomes "Admin" ou "Student" conforme o seu banco de dados
                    case "admin":
                        _navigationManager.NavigateTo("/admin/dashboard");
                        break;
                    case "cliente":
                        _navigationManager.NavigateTo("/cliente/carrinho");
                        break;
                    default:
                        _navigationManager.NavigateTo("/login");
                        break;
                }
            }

        }

        public async Task Cadastro(CadastroRequestDto dto)
        {
            if (dto.Senha != dto.confirmSenha)
                throw new ArgumentException("Senhas não conferes");
            var response = await _http.PostAsJsonAsync("api/Usuarios/", dto);
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException("Erro ao cadastrar usuario");
            var LoginDto = new LoginDto
            {
                Email = dto.Email,
                Senha = dto.Senha,
            };
            await Login(LoginDto);
        }
    }
}
