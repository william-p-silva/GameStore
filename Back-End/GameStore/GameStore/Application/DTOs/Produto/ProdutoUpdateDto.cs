using GameStore.Domain.Entities;

namespace GameStore.Application.DTOs.Produto
{
    public class ProdutoUpdateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public bool Ativo { get; set; } = true;
        public int CategoriaId { get; set; }
    }
}
