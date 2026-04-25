namespace Blazor.DTOs.Produto
{
    public class ProdutoFiltroDto
    {
        public int? Page {  get; set; }
        public int? PageSize { get; set; }
        public string? Nome { get; set; }
        public int? CategoriaId { get; set; }
    }
}
