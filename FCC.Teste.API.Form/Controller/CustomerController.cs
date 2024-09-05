using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Core.Repositories;
using FCC.Teste.Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FCC.Teste.API.Form.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        protected string GetClaim(string name) => User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value!;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository; 
        }

        [HttpGet]
        [EndpointSummary("Retorna uma lista de todos os clientes cadastrados no sistema.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<ReadCustomerDto>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllCustomer()
        {
            try
            {
                var users = _customerRepository.GetAll();

                if (users is null)
                {
                    return NotFound();
                }

                return Ok(new { users });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtém um cliente pelo Id.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ReadCustomerDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                var user = _customerRepository.GetById(new ReadCustomerDto { Id = id });

                if (user is null)
                {
                    return NotFound();
                }

                return Ok(new { user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [EndpointSummary("Cria um novo cliente.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ReadCustomerDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create([FromBody] CreateCustomerDto dto)
        {
            try
            {
                dto.UserId = GetClaim(ClaimTypes.Name);

                var customerCreated = _customerRepository.Create(dto);

                if (customerCreated is null)
                {
                    return NotFound();
                }

                return CreatedAtAction(null, customerCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [EndpointSummary("Atualiza um cliente existente.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ReadCustomerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] UpdateCustomerDto dto)
        {
            try
            {
                dto.UserId = GetClaim(ClaimTypes.Name);

                var updateCustomer = _customerRepository.Update(dto);

                if (updateCustomer is null)
                {
                    return NotFound();
                }

                return Ok(updateCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        [EndpointSummary("Exclui um cliente.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromBody] DeleteCustomerDto dto)
        {
            try
            {
                var customer = _customerRepository.Delete(dto);

                if (customer is null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
