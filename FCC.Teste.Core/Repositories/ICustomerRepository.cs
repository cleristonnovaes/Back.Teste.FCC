using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Core.Models;
using FCC.Teste.Core.Response;

namespace FCC.Teste.Core.Repositories
{
    public interface ICustomerRepository
    {
        Response<ReadCustomerDto> Create(CreateCustomerDto dto);
        Response<ReadCustomerDto> GetById(ReadCustomerDto id);
        Response<List<ReadCustomerDto>> GetAll();
        Response<ReadCustomerDto> Update(UpdateCustomerDto dto);
        Response<ReadCustomerDto?> Delete (DeleteCustomerDto id);

    }
}
