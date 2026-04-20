namespace GameStore.Application.DTOs.Produto
{
    public class ProdutoFiltroDto
    {
        public string? Nome { get; set; }
        public int? CategoriaId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
