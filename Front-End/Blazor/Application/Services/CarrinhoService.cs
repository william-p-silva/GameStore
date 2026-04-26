using Blazor.Application.DTOs;
using Blazor.Application.DTOs.Carrinho;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Blazor.Application.Services
{
    public class CarrinhoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly NavigationManager _navigationManager; // Adicione este campo
        public CarrinhoService(IHttpClientFactory clientFactory, NavigationManager navigationManager)
        {
            _clientFactory = clientFactory;
            _navigationManager = navigationManager;
        }

        public async Task<CarrinhoResponseDto> AdicionarProdutoAoCarrinho(AdicionarProdutoRequestDto dto)
        {
            var http = _clientFactory.CreateClient("Privado");

            var response = await http.PostAsJsonAsync("api/Carrinho/adicionarItem", dto);

            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadFromJsonAsync<ApiResponseSemPage<CarrinhoResponseDto>>();

                if (resultado != null && resultado.Sucesso)
                {
                    return resultado.Dados;
                }
                throw new Exception(string.Join(", ", resultado?.Erros ?? new List<string> { "Erro desconhecido" }));
            }
            // Se cair aqui, é erro de autorização ou servidor (401, 500, etc)
            throw new Exception("Erro ao comunicar com o servidor.");
        }



    }
}
