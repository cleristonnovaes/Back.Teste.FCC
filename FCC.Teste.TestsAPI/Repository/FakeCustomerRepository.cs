using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Core.Repositories;
using FCC.Teste.Core.Response;

namespace FCC.Teste.TestsAPI.Repository
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        private static List<ReadCustomerDto> customers;

        public FakeCustomerRepository()
        {
            customers = new List<ReadCustomerDto>
            {
                new ReadCustomerDto { Id = 1, Cpf = "12345678901", Name = "João da Silva" },
                new ReadCustomerDto { Id = 2, Cpf = "98765432101", Name = "Maria da Costa" }
            };
        }

        public Response<ReadCustomerDto> Create(CreateCustomerDto dto)
        {
            var nextId = customers.Max(c => c.Id) + 1;
            var newCustomer = new ReadCustomerDto
            {
                Id = nextId,
                Cpf = dto.Cpf,
                Name = dto.Name
            };

            customers.Add(newCustomer);
            return new Response<ReadCustomerDto>(newCustomer);
        }

        public Response<ReadCustomerDto?> Delete(DeleteCustomerDto dto)
        {
            customers.RemoveAll(c => c.Id == dto.Id);
            return new Response<ReadCustomerDto?>(null, 204, message: "Excluido");
        }

        public Response<List<ReadCustomerDto>> GetAll()
        {
            return new Response<List<ReadCustomerDto>>(customers);
        }

        public Response<ReadCustomerDto> GetById(ReadCustomerDto dto)
        {
            return new Response<ReadCustomerDto>(customers.FirstOrDefault(c => c.Id == dto.Id));
        }

        public Response<ReadCustomerDto> Update(UpdateCustomerDto dto)
        {
            var existCustomer = customers.FirstOrDefault(c => c.Id == dto.Id);
            if (existCustomer != null)
            {
                existCustomer.Cpf = dto.Cpf;
                existCustomer.Name = dto.Name;
                return new Response<ReadCustomerDto>(existCustomer);
            }
            return new Response<ReadCustomerDto>(null, 500, "Erro");
        }
    }
}
