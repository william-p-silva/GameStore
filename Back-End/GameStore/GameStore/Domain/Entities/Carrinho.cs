namespace GameStore.Domain.Entities
{
    public class Carrinho
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public List<CarrinhoItem> Itens { get; set; } = new();

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
