using FCC.Teste.Core.Models;
using FCC.Teste.Core.Response;

namespace FCC.Teste.Core.Services
{
    public interface ILoginService
    {
        Response<TokenModel> GenerateToken(User user);
    }
}
