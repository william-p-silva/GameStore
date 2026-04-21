namespace GameStore.Application.DTOs.Carrinho
{
    public class CarrinhoItemResponseDto
    {
        public int PrudutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}
