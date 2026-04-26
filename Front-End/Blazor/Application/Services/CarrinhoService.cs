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
            try
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
                throw new ArgumentException("Erro ao comunicar com o servidor.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao comunicar com o servidor: {ex.Message}");
            }
           
        }


        public async Task<CarrinhoResponseDto> ObterCarrinho()
        {

            try
            {
                var http = _clientFactory.CreateClient("Privado");
                var response = await http.GetFromJsonAsync<ApiResponseSemPage<CarrinhoResponseDto>>("api/Carrinho/carrinho");

                if (response != null && response.Sucesso)
                {
                    return response.Dados;
                }
                throw new ArgumentException("Erro ao comunicar com o servidor.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao comunicar com o servidor: {ex.Message}");
            }
            
        }


        public async Task<CarrinhoResponseDto> RemoverProduto(int produtoId)
        {
            try
            {
                var http = _clientFactory.CreateClient("Privado");

                var dto = new RemoverProdutoDto
                {
                    ProdutoId = produtoId,
                };

                var request = new HttpRequestMessage(HttpMethod.Delete, "api/Carrinho/removerItem");
                request.Content = JsonContent.Create(dto);

                var response = await http.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var carrinho = await response.Content.ReadFromJsonAsync<ApiResponseSemPage<CarrinhoResponseDto>>();

                    if (carrinho != null && carrinho.Sucesso)
                    {
                        return carrinho.Dados;
                    }
                    throw new ArgumentException("Erro no carrinho");
                }
                throw new ArgumentException("Erro ao comunicar com o servidor.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao comunicar com o servidor: {ex.Message}");
            }
            
        }

        public async Task<CarrinhoResponseDto> AtualizarQuantidade(AtualizarItemRequestDto dto)
        {
            Console.WriteLine(dto.ProdutoId);
            var http = _clientFactory.CreateClient("Privado");
            var response = await http.PutAsJsonAsync("api/Carrinho/atualizarItem", dto);
            if (response.IsSuccessStatusCode)
            {
                var carrinho = await response.Content.ReadFromJsonAsync<ApiResponseSemPage<CarrinhoResponseDto>>();

                if(carrinho != null && carrinho.Sucesso)
                {
                    return carrinho.Dados;
                }
                throw new ArgumentException("Erro.");
            }
            throw new ArgumentException("Erro ao comunicar com o servidor.");
        }

    }
}
