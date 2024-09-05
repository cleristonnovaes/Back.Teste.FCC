using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Core.Repositories;
using FCC.Teste.TestsAPI.Repository;

namespace FCC.Teste.TestsAPI
{
    [TestClass]
    public class CustomersRepositoryTests
    {
        
        private ICustomerRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new FakeCustomerRepository();
        }

        [TestMethod]
        public void GetAllCustomer_ReturnsAllCustomers()
        {
            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Data.Count);
        }

        [TestMethod]
        public void GetById_Existing_Customer_Return_Customer()
        {
            // Act
            var result = _repository.GetById(new ReadCustomerDto { Id = 1 });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Data.Id);
            Assert.AreEqual("12345678901", result.Data.Cpf);
            Assert.AreEqual("João da Silva", result.Data.Name);
        }

        [TestMethod]
        public void GetById_NonExistingCustomer_ReturnsNull()
        {
            // Act
            var resultado = _repository.GetById(new ReadCustomerDto { Id = 99 });

            // Assert
            Assert.IsNull(resultado.Data);
        }

        [TestMethod]
        public void CreateCliente_CreatesNewCliente()
        {
            // Arrange
            var customerNew = new CreateCustomerDto { Cpf = "99999999999", Name = "Teste Cliente" };

            // Act
            var result = _repository.Create(customerNew);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Data.Id); 
            Assert.AreEqual(customerNew.Cpf, result.Data.Cpf);
            Assert.AreEqual(customerNew.Name, result.Data.Name);
        }

        [TestMethod]
        public void UpdateCliente_UpdatesExistingCliente()
        {
            // Arrange
            var updateCustomer = new UpdateCustomerDto { Id = 1, Cpf = "12345678901", Name = "João da Silva Atualizado" };

            // Act
            var result = _repository.Update(updateCustomer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updateCustomer.Id, result.Data.Id);
            Assert.AreEqual(updateCustomer.Cpf, result.Data.Cpf);
            Assert.AreEqual(updateCustomer.Name, result.Data.Name);
        }

        [TestMethod]
        public void DeleteCliente_DeletesExistingCliente()
        {
            // Arrange
            var customerId = 1;

            // Act
            _repository.Delete(new DeleteCustomerDto { Id = customerId});

            // Assert
            Assert.IsFalse(_repository.GetAll().Data.Any(c => c.Id == customerId)); 
        }
    }
}