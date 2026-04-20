namespace GameStore.Application.DTOs.Produto
{
    public class ProdutoFiltroDto : PaginacaoDto
    {
        public string? Nome { get; set; }
        public int? CategoriaId { get; set; }

    }
}
