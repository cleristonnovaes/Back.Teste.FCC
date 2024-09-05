using FCC.Teste.Core.Models;

namespace FCC.Teste.Core.DTOs.Customers
{
    public class ReadCustomerDto
    {
        public int Id { get; set; }
        public string Cpf { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string RG { get; set; } = null!;
        public string DispatchOrgan { get; set; } = null!;
        public DateTime? DispatchDate { get; set; }
        public string DispatchState { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string MaritalStatus { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; } = null!;
    }
}
