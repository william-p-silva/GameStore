using Blazor.Application.DTOs.Produto;

namespace Blazor.Application.DTOs.Carrinho
{
    public class CarrinhoResponseDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioEmail { get; set; } = string.Empty;
        public int TotalItens { get; set; }
        public decimal TotalCarrinho { get; set; }
        public List<ProdutoResumoDto> Itens { get; set; } = new();
        public decimal Frete => TotalCarrinho * 0.20m;
        public decimal Total => Frete + TotalCarrinho;
    }
}
