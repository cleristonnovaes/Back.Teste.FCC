using FCC.Teste.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FCC.Teste.Core.DTOs.Customers
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [Length(14, 14, ErrorMessage = "Infomar um número de CPF válido")]
        public string Cpf { get; set; } = null!;
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "A Data de Nascimento é obrigatório.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "O RG é obrigatório.")]
        public string RG { get; set; } = null!;
        [Required(ErrorMessage = "O Órgão Expedição é obrigatório.")]
        public string DispatchOrgan { get; set; } = null!;
        [Required(ErrorMessage = "A Data de Expedição é obrigatória.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DispatchDate { get; set; }
        [Required(ErrorMessage = "O Estado de expedição é obrigatório.")]
        public string DispatchState { get; set; } = null!;
        [Required(ErrorMessage = "O Sexo é obrigatório.")]
        public string Gender { get; set; } = null!;
        [Required(ErrorMessage = "O Estado Civil é obrigatório.")]
        public string MaritalStatus { get; set; } = null!;
        [JsonIgnore]
        public int AddressId { get; set; }
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [Length(9, 9, ErrorMessage = "Infomar um número de CEP válido")]
        public string ZipCode { get; set; } = null!;
        [Required(ErrorMessage = "O Logradouro é obrigatório.")]
        public string Street { get; set; } = null!;
        [Required(ErrorMessage = "O Número é obrigatório.")]
        public string? StreetNumber { get; set; }
        public string? Complement { get; set; }
        [Required(ErrorMessage = "O Bairro é obrigatório.")]
        public string Neighborhood { get; set; } = null!;
        [Required(ErrorMessage = "A Cidade é obrigatório.")]
        public string City { get; set; } = null!;
        [Required(ErrorMessage = "O Estado é obrigatório.")]
        public string State { get; set; } = null!;
        [JsonIgnore]
        public string UserId { get; set; } = string.Empty!;
    }
}
