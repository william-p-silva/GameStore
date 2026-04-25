using Blazor.DTOs;
using Blazor.DTOs.Produto;
using System.Net.Http.Json;

namespace Blazor.Services
{
    public class ProdutoService
    {
        private readonly HttpClient _http;
        public ProdutoService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<ProdutoResponseDto>> Listar()
        {

            var response = await _http.GetFromJsonAsync<ApiResponse<ProdutoResponseDto>>("api/Produtos");
            if (response != null && response.Sucesso && response.Dados != null)
            { 
                return response.Dados.Data;
            }

            return new List<ProdutoResponseDto>();
        }
    }
}
