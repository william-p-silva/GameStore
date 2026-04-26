namespace Blazor.Application.DTOs.Produto
{
    public class ProdutoResumoDto
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}
