using GameStore.Domain.Entities;

namespace GameStore.Application.DTOs
{
    public class ProdutoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public bool Ativo { get; set; } = true;
        public string CategoriaNome { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
    }
}
