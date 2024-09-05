using FCC.Teste.Core.Models;
using FCC.Teste.Core.Repositories;

namespace FCC.Teste.API.Auth.Repository
{
    public class UserRepository : IUserRepository
    {

        public User GetUser(string username, string password)
        {
            return _users.FirstOrDefault(x => x.UserName == username && x.Password == password)!;
        }

        private readonly List<User> _users = new List<User>
        {
            new User { UserId = "1", UserName = "admin", Password = "123456" }
        };
    }
}
