namespace FCC.Teste.Core.Response
{
    public class CustomerResponseList
    {
        public UsersResponse Users { get; set; }
        public class Address
        {
            public int Id { get; set; }
            public string Cep { get; set; }
            public string Logradouro { get; set; }
            public string StreetNumber { get; set; }
            public string Complement { get; set; }
            public string Bairro { get; set; }
            public string Localidade { get; set; }
            public string Uf { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime ModifiedAt { get; set; }
            public string ModifiedBy { get; set; }
        }

        public class Customer
        {
            public int Id { get; set; }
            public string Cpf { get; set; }
            public string Name { get; set; }
            public DateTime BirthDate { get; set; }
            public string Rg { get; set; }
            public string DispatchOrgan { get; set; }
            public DateTime DispatchDate { get; set; }
            public string DispatchState { get; set; }
            public string Gender { get; set; }
            public string MaritalStatus { get; set; }
            public Address Address { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime ModifiedAt { get; set; }
            public string ModifiedBy { get; set; }
        }

        public class UsersResponse
        {
            public List<Customer> Data { get; set; }
            public string Message { get; set; }
        }
    }
}
