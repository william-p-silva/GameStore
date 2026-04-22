using GameStore.Domain.Entities;

namespace GameStore.Application.DTOs.Pedido
{
    public class PedidoResponseDto
    {
        public int Id { get; set; }

        public string NumeroPedido => $"PED-{Id:000000}";

        public string Status { get; set; } = string.Empty;

        public DateTime CriadoEm { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Frete => SubTotal * 0.20m;
        public decimal Total => SubTotal + Frete;

        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; } = string.Empty;

        public List<PedidoItemResponseDto> Itens { get; set; } = new();


    }
}
