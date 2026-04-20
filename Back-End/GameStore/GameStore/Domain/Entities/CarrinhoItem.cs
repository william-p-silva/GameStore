namespace GameStore.Domain.Entities
{
    public class CarrinhoItem
    {
        public int Id { get; set; }
        public int CarrinhoId { get; set; }
        public Carrinho? Carrinho { get; set; }
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
