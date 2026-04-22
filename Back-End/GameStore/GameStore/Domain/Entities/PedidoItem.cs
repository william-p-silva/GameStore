namespace GameStore.Domain.Entities
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
