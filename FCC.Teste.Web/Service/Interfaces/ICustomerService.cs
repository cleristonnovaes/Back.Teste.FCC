using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Core.Response;

namespace FCC.Teste.Web.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> Create(CreateCustomerDto dto, string token);
        Task<bool> Update(UpdateCustomerDto dto, string token);
        Task<bool> Delete(DeleteCustomerDto dto, string token);
        Task<UpdateCustomerDto> GetById(int id, string token);
        Task<CustomerResponseList>GetAll(string token);


    }
}
