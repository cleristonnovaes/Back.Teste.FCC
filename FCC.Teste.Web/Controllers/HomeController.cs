using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Web.Models;
using FCC.Teste.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace FCC.Teste.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerService _customerService;

        public HomeController(ILogger<HomeController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var jwtToken = HttpContext.Session.GetString("JwtToken");

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return View();

            }
            var response = await _customerService.GetAll(jwtToken!);

            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
