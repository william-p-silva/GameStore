namespace GameStore.Application.DTOs.Pedido
{
    public class PedidofiltroAdminDto : PaginacaoDto
    {
        public string? Status { get; set; }
        public int? UsuarioId { get; set; }
    }
}
