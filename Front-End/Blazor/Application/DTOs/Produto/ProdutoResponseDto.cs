namespace Blazor.Application.DTOs.Produto
{
    public class ProdutoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao {  get; set; } = string.Empty;
        public decimal Preco {  get; set; } = decimal.Zero;
        public int Estoque { get; set; }
        public bool Ativo { get; set; }
        public string CategoriaNome {  get; set; } = string.Empty;
        public int CategoriaId { get; set; }
    }
}
