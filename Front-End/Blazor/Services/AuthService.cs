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
            var response = await _http.PostAsJsonAsync("api/usuario/login", new
            {
                Email = email,
                Senha = senha
            });

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro no login");

            var token = await response.Content.ReadAsStringAsync();

            return token;
        }
    }
}
