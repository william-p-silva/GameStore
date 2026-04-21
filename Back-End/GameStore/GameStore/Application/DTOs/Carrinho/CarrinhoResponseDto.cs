using GameStore.Domain.Entities;

namespace GameStore.Application.DTOs.Carrinho
{
    public class CarrinhoResponseDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioEmail { get; set; } = string.Empty;
        public List<CarrinhoItemResponseDto> Itens { get; set; } = new();
        public int TotalItens { get; set; }
        public decimal TotalCarrinho { get; set; }
    }
}
