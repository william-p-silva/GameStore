namespace GameStore.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public List<PedidoItem> Itens { get; set; } = new();

        public decimal Total { get; set; }
        public string Status { get; set; } = "Pendente";

        public DateTime CriadoEm = DateTime.UtcNow;
    }
}
