using FCC.Teste.Core.Models;

namespace FCC.Teste.Web.Service.Interfaces
{
    public interface IViaCepService
    {
        Task<Address?> SearchAddress(string zipcode);
    }
}
