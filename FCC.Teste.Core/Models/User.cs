using System.ComponentModel.DataAnnotations;

namespace FCC.Teste.Core.Models
{
    public class User
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
