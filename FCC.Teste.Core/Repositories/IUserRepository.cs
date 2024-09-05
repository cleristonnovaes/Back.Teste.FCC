using FCC.Teste.Core.Models;

namespace FCC.Teste.Core.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
    }
}