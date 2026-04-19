using GameStore.Domain.Entities;

namespace GameStore.Application.DTOs
{
    public class CategoriaResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<ProdutoResumoDto> Produtos { get; set; } = new();
    }
}
