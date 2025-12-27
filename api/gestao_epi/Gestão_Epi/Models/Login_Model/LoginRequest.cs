using System.ComponentModel.DataAnnotations;

namespace Gestão_Epi.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string? Email { get; set; } = string.Empty;
        public required string Senha { get; set; } = string.Empty;


    }
}
