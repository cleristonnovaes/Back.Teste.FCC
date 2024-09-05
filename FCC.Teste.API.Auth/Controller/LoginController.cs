using FCC.Teste.Core.Models;
using FCC.Teste.Core.Request;
using FCC.Teste.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FCC.Teste.API.Auth.Controller
{
    [ApiController]
    [Route("auth/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginRepository)
        {
            _loginService = loginRepository;  
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = _loginService.GenerateToken(new User { UserName = request.UserName, Password = request.Password });
                if(token is null)
                    return Unauthorized();

                return Ok( new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
