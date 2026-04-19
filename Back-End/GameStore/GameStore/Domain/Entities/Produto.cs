using System.Text.Json.Serialization;

namespace GameStore.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public bool Ativo { get; set; } = true;


        //Relacionamento FK
        public int? CategoriaId {  get; set; }
        [JsonIgnore]
        public Categoria? Categoria { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
