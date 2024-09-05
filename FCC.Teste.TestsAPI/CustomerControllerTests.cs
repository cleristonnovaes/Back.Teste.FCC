using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Core.Repositories;
using FCC.Teste.Core.Response;
using FCC.Teste.TestsAPI.Controller;
using FCC.Teste.TestsAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FCC.Teste.TestsAPI
{
    [TestClass]
    public class CustomerControllerTests
    {
        private ICustomerRepository _repository;
        private FakeCustomerController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new FakeCustomerRepository();
            _controller = new FakeCustomerController(_repository);
        }

        [TestMethod]
        public void GetAllCustomer_ReturnsOkResultWithListOfCustomers()
        {
            // Act
            var result = _controller.GetAllCustomer();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            Assert.IsInstanceOfType(okResult?.Value, typeof(Response<List<ReadCustomerDto>>));

            var returnedClientes = okResult?.Value as Response<List<ReadCustomerDto>>;
            Assert.AreEqual(2, returnedClientes?.Data.Count);
        }

        [TestMethod]
        public void GetCustomerById_ExistingCustomer_ReturnsOkResultWithCustomer()
        {
            // Act
            var result = _controller.GetCustomerById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            Assert.IsInstanceOfType(okResult?.Value, typeof(Response<ReadCustomerDto>));

            var returnedCliente = okResult?.Value as Response<ReadCustomerDto>;
            Assert.AreEqual(1, returnedCliente?.Data.Id);
            Assert.AreEqual("12345678901", returnedCliente?.Data.Cpf);
            Assert.AreEqual("João da Silva", returnedCliente?.Data.Name);
        }

        [TestMethod]
        public void GetCustomerById_NonExistingCustomer_ReturnsNotFoundResult()
        {
            // Act
            var result = _controller.GetCustomerById(99);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public void CreateCustomer_Valid_ReturnsCreatedAtActionResultWithCreatedCustomer()
        {
            // Arrange
            var customerNew = new CreateCustomerDto { Cpf = "99999999999", Name = "Teste Cliente" };

            // Act
            var result = _controller.Create(customerNew);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var createdAtResult = result as OkObjectResult;
            Assert.IsNotNull(createdAtResult?.Value);
            Assert.IsInstanceOfType(createdAtResult?.Value, typeof(Response<ReadCustomerDto>));

            var returnedCliente = createdAtResult?.Value as Response<ReadCustomerDto>;
            Assert.AreEqual(3, returnedCliente?.Data.Id);
            Assert.AreEqual("99999999999", returnedCliente?.Data.Cpf);
            Assert.AreEqual("Teste Cliente", returnedCliente?.Data.Name);
        }

        [TestMethod]
        public void UpdateCustomer_Valid_ReturnsOkResultWithUpdatedCustomer()
        {
            // Arrange
            var updateCustomer = new UpdateCustomerDto { Id = 1, Cpf = "12345678901", Name = "João da Silva Atualizado" };

            // Act
            var result = _controller.Update(updateCustomer);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            Assert.IsInstanceOfType(okResult?.Value, typeof(Response<ReadCustomerDto>));

            var returnedCliente = okResult?.Value as Response<ReadCustomerDto>;
            Assert.AreEqual(1, returnedCliente?.Data.Id);
            Assert.AreEqual("12345678901", returnedCliente?.Data.Cpf);
            Assert.AreEqual("João da Silva Atualizado", returnedCliente?.Data.Name);
        }

        [TestMethod]
        public void UpdateCustomer_InvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var updatedCustomer = new UpdateCustomerDto { Id = 2, Cpf = "98765432101", Name = "Maria da Costa Atualizada" };

            // Act
            var result = _controller.Update(updatedCustomer);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public void DeleteCustomer_ExistingCustomer_ReturnsNoContentResult()
        {
            // Act
            var result = _controller.Delete(new DeleteCustomerDto { Id = 1});

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

    }
}
