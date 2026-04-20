using GameStore.Application.DTOs.Produto;
using GameStore.Domain.Entities;

namespace GameStore.Application.DTOs.Categoria
{
    public class CategoriaResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<ProdutoResumoDto> Produtos { get; set; } = new();
        public int? QuantidadeProdutos { get; set; }
    }
}
