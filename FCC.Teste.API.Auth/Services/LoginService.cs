using FCC.Teste.Core.Models;
using FCC.Teste.Core.Repositories;
using FCC.Teste.Core.Response;
using FCC.Teste.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FCC.Teste.API.Auth.Repositories
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public Response<TokenModel> GenerateToken(User user)
        {
			try
			{
                var login = _userRepository.GetUser(user.UserName, user.Password);

                if (login == null)
                {
                    return new Response<TokenModel>(null, 404, message: "usuário não encontrado"); ;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Secret")!));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.GivenName, login.UserName),
                        new Claim(ClaimTypes.Name, login.UserId)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var responseToken = new TokenModel { Token = tokenHandler.WriteToken(token), UserId = login.UserId };

                return  new Response<TokenModel>(responseToken, message: "logado com sucesso");
            }
			catch 
			{
                return new Response<TokenModel>(null, 500, message: "Erro ao logar");
            }
        }
    }
}
