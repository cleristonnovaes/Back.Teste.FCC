using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FCC.Teste.Core.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [JsonPropertyName("cep")]
        public string ZipCode { get; set; } = null!;
        [Required(ErrorMessage = "O Logradouro é obrigatório.")]
        [JsonPropertyName("logradouro")]
        public string Street { get; set; } = null!;
        [Required(ErrorMessage = "O Número é obrigatório.")]
        public string? StreetNumber { get; set; }
        public string? Complement {  get; set; }
        [Required(ErrorMessage = "O Bairro é obrigatório.")]
        [JsonPropertyName("bairro")]
        public string Neighborhood { get; set; } = null!;
        [Required(ErrorMessage = "A Cidade é obrigatório.")]
        [JsonPropertyName("localidade")]
        public string City { get; set; } = null!;
        [Required(ErrorMessage = "O Estado é obrigatório.")]
        [JsonPropertyName("uf")]
        public string State { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; } = null!;
    }
}
