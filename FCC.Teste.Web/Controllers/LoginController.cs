using FCC.Teste.Core.Models;
using FCC.Teste.Core.Request;
using FCC.Teste.Core.Response;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FCC.Teste.Web.Controllers
{
    [Route("[controller]")]
    public class LoginController  : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest loginModel)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("LoginAPI");
                var response = await client.PostAsJsonAsync("auth/login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseString);

                    if (tokenResponse?.Token.Data != null)
                    {
                        HttpContext.Session.SetString("JwtToken", tokenResponse.Token.Data.Token!);
                        HttpContext.Session.SetString("Username", loginModel.UserName);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        return View("Error", loginModel);
                    }

                }
                else
                {
                    return View("Error", loginModel);
                }
            }
            return View("index", loginModel);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JwtToken");
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }

    }
}
