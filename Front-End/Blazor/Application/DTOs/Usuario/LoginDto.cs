using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Application.DTOs.Usuario
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é um campo obrigatorio")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é um campo obrigatorio")]
        [PasswordPropertyText]
        public string Senha { get; set; } = string.Empty;
    }
}
