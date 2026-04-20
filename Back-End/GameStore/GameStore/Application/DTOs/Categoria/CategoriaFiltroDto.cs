namespace GameStore.Application.DTOs.Categoria
{
    public class CategoriaFiltroDto : PaginacaoDto
    {
        public string? Nome { get; set; }
        public bool? Produtos { get; set; } = true;


    }
}
