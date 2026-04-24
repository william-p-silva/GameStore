using System.Net.Http.Json;

namespace Blazor.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }


        public async Task<string> Login(string email, string senha)
        {
            var loginData = new { email = email, senha = senha }; // Usando nomes em minúsculo
            var response = await _http.PostAsJsonAsync("api/Usuarios/login", loginData);

            if (!response.IsSuccessStatusCode)
            {
                // Isso vai te mostrar no console do navegador se foi 400, 404, 500, etc.
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro {response.StatusCode}: {errorContent}");
            }

            var resultado = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return resultado.Token;
        }
    }
    // Classe auxiliar para o mapeamento
    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
