namespace GameStore.Application.DTOs.Pedido
{
    public class PedidoItemResponseDto
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public decimal SubTotal => Quantidade * PrecoUnitario;
    }
}
