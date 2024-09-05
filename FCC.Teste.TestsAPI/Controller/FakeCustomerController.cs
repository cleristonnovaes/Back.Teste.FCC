using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Core.Repositories;
using FCC.Teste.Core.Response;
using FCC.Teste.TestsAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FCC.Teste.TestsAPI.Controller
{
    public class FakeCustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public FakeCustomerController(ICustomerRepository customerRepository)
        {
           _customerRepository = new FakeCustomerRepository();
        }

        public IActionResult GetAllCustomer()
        {
            var customers = _customerRepository.GetAll();
            return Ok(customers);
        }

        public IActionResult GetCustomerById(int id)
        {
            var customer = _customerRepository.GetById(new ReadCustomerDto { Id = id });
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        public IActionResult Create(CreateCustomerDto dto)
        {
            var customerCreated = _customerRepository.Create(dto);

            return Ok(customerCreated);
        }

        public IActionResult Update(UpdateCustomerDto dto)
        {

            var updateCustomer = _customerRepository.Update(dto);

            if (updateCustomer is null)
            {
                return NotFound();
            }

            return Ok(updateCustomer);
        }

        public IActionResult Delete(DeleteCustomerDto dto)
        {
            var customer = _customerRepository.Delete(dto);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}
