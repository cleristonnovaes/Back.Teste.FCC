using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FCC.Teste.Core.DTOs.Customers
{
    public class UpdateCustomerDto
    {
        public int Id { get; set; }
        public string Cpf { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string RG { get; set; } = null!;
        public string DispatchOrgan { get; set; } = null!;
        public DateTime DispatchDate { get; set; }
        public string DispatchState { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string MaritalStatus { get; set; } = null!;
        [JsonIgnore]
        public int AddressId { get; set; }
        public string ZipCode { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string? StreetNumber { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        [JsonIgnore]
        public string UserId { get; set; } = string.Empty!;
    }
}
