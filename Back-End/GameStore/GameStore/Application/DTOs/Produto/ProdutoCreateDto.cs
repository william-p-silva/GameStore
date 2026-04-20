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

        [Range(1, double.MaxValue, ErrorMessage = "Preco deve ser maior que zero")]
        public decimal Preco { get; set; }

        [Range(0, int.MaxValue)]
        public int Estoque { get; set; }
        public bool Ativo { get; set; } = true;
        public int CategoriaId { get; set; }
    }
}
