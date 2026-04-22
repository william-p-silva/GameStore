using GameStore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Application.DTOs.Produto
{
    public class ProdutoCreateDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;
       
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public bool Ativo { get; set; } = true;
        public int CategoriaId { get; set; }
    }
}
