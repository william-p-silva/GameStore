namespace Blazor.Application.DTOs.Carrinho
{
    public class AdicionarProdutoRequestDto
    {
        public int ProdutoID { get; set; }
        public int Quantidade { get; set; } = 1;
    }
}
