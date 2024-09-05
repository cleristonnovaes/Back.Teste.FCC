using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Core.Models;
using FCC.Teste.Core.Response;
using FCC.Teste.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCC.Teste.Web.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {

        private readonly ICustomerService _customerService;
        private readonly IViaCepService _viaCepService;

        public CustomerController(IViaCepService viaCepService, ICustomerService customerService)
        {
            _viaCepService = viaCepService;
            _customerService = customerService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerDto customer)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var response = await _customerService.Create(customer, token!);

                if (response)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Error", customer);
                }
            }
            return View("index", customer);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var response = await _customerService.GetById(id, token!);

                if (response != null)
                {
                    return View("details", response);
                }
                else
                {
                    return View("Error", new ReadCustomerDto());
                }
            }
            return View("index", new ReadCustomerDto());
        }


        [HttpPut("update")]
        [Route("update", Name = "UpdateCustomerRoute")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCustomerDto customer)
        {

            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var response = await _customerService.Update(customer, token!);

                if (response)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("ErrorDetails", customer);
                }
            }
            return View("details", customer);
        }

        [HttpDelete("delete")]
        [Route("delete/{id}", Name = "DeleteCustomerRoute")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = new DeleteCustomerDto { Id = id };
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var response = await _customerService.Delete(customer, token!);

                if (response)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Error", customer);
                }
            }
            return View("index", customer);
        }

        [HttpPost]
        [Route("FillAutoAddress")]
        public async Task<IActionResult> FillAutoAddress(string zipcode)
        {
            var endereco = new Address();
            if (!string.IsNullOrEmpty(zipcode) && zipcode.Length == 9)
            {
                zipcode = zipcode.Replace("-", "").Trim();

                endereco = await _viaCepService.SearchAddress(zipcode);

                if (endereco.Street != null)
                {
                    return Json(new { sucesso = true, endereco });
                }
            }

            return Json(new { sucesso = false, endereco });
        }
    }
}
