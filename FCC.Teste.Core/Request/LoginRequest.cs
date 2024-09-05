using System.ComponentModel.DataAnnotations;

namespace FCC.Teste.Core.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Informar o usuário.")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informar a senha.")]
        public string Password { get; set; } = string.Empty;
    }
}
