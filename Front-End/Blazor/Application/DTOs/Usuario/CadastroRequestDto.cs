using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Application.DTOs.Usuario
{
    public class CadastroRequestDto
    {
        [Required(ErrorMessage ="Nome é um campo obrigatorio")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é um campo obrigatorio")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é um campo obrigatorio")]
        [PasswordPropertyText]
        public string Senha {get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmar Senha é um campo obrigatorio")]
        [PasswordPropertyText]
        public string confirmSenha { get; set; } = string.Empty;
    }
}
