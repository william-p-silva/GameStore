namespace GameStore.Application.DTOs.Usuario
{
    public class UsuarioFiltroDto : PaginacaoDto
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        
    }
}
