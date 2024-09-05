using System.ComponentModel.DataAnnotations;

namespace FCC.Teste.Core.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string Cpf { get; set; } = null!;
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "A Data de Nascimento é obrigatório.")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "O RG é obrigatório.")]
        public string RG { get; set; } = null!;
        [Required(ErrorMessage = "O Órgão Expedição é obrigatório.")]
        public string DispatchOrgan { get; set; } = null!;
        [Required(ErrorMessage = "A Data de Expedição é obrigatória.")]
        public DateTime DispatchDate { get; set; }
        [Required(ErrorMessage = "O Estado de expedição é obrigatório.")]
        public string DispatchState { get; set; } = null!;
        [Required(ErrorMessage = "O Sexo é obrigatório.")]
        public string Gender { get; set; } = null!;
        [Required(ErrorMessage = "O Estado Civil é obrigatório.")]
        public string MaritalStatus { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public int AddressId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }  = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; } = null!;
        
    }
}
