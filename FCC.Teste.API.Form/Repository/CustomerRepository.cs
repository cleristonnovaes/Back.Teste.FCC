using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Core.Models;
using FCC.Teste.Core.Repositories;
using FCC.Teste.Core.Response;
using System.Data.SqlClient;

namespace FCC.Teste.API.Form.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:DefaultConnection"]!;
        }
        public Response<ReadCustomerDto> Create(CreateCustomerDto dto)
        {

            var response = new ReadCustomerDto();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand(
                                           "INSERT INTO Address (Zipcode, Street, StreetNumber, Complement, Neighborhood, City, State, CreatedBy, CreatedAt, ModifiedBy, ModifiedAt) " +
                                           "VALUES (@ZipCode, @Street, @StreetNumber, @Complement, @Neighborhood, @City, @State, @CreatedBy, @CreatedAt, @ModifiedBy, @ModifiedAt); " +
                                           "SELECT SCOPE_IDENTITY();", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ZipCode", dto.ZipCode);
                            command.Parameters.AddWithValue("@Street", dto.Street);
                            command.Parameters.AddWithValue("@StreetNumber", dto.StreetNumber);
                            command.Parameters.AddWithValue("@Complement", dto.Complement ?? string.Empty);
                            command.Parameters.AddWithValue("@Neighborhood", dto.Neighborhood);
                            command.Parameters.AddWithValue("@City", dto.City);
                            command.Parameters.AddWithValue("@State", dto.State);
                            command.Parameters.AddWithValue("@CreatedBy", dto.UserId);
                            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                            command.Parameters.AddWithValue("@ModifiedBy", dto.UserId);
                            command.Parameters.AddWithValue("@ModifiedAt", DateTime.Now);

                            dto.AddressId = Convert.ToInt32(command.ExecuteScalar());
                        }

                        using (var command = new SqlCommand(
                            "INSERT INTO Customer (Cpf, Name, BirthDate , RG , DispatchOrgan, DispatchDate, DispatchState, Gender, MaritalStatus, AddressId,  CreatedBy, CreatedAt, ModifiedBy, ModifiedAt) " +
                            "VALUES (@Cpf, @Name, @BirthDate, @RG, @DispatchOrgan, @DispatchDate, @DispatchState, @Gender, @MaritalStatus, @AddressId, @CreatedBy, @CreatedAt, @ModifiedBy, @ModifiedAt) " +
                            "SELECT SCOPE_IDENTITY();", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Cpf", dto.Cpf);
                            command.Parameters.AddWithValue("@Name", dto.Name);
                            command.Parameters.AddWithValue("@BirthDate", dto.BirthDate);
                            command.Parameters.AddWithValue("@RG", dto.RG);
                            command.Parameters.AddWithValue("@DispatchOrgan", dto.DispatchOrgan);
                            command.Parameters.AddWithValue("@DispatchDate", dto.DispatchDate);
                            command.Parameters.AddWithValue("@DispatchState", dto.DispatchState);
                            command.Parameters.AddWithValue("@Gender", dto.Gender);
                            command.Parameters.AddWithValue("@MaritalStatus", dto.MaritalStatus);
                            command.Parameters.AddWithValue("@AddressId", dto.AddressId);
                            command.Parameters.AddWithValue("@CreatedBy", dto.UserId);
                            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                            command.Parameters.AddWithValue("@ModifiedBy", dto.UserId);
                            command.Parameters.AddWithValue("@ModifiedAt", DateTime.Now);

                            response.Id = Convert.ToInt32(command.ExecuteScalar());
                        }

                        response.Cpf = dto.Cpf;
                        response.Name = dto.Name;
                        response.BirthDate = dto.BirthDate;
                        response.RG = dto.RG;
                        response.DispatchOrgan = dto.DispatchOrgan;
                        response.DispatchDate = dto.DispatchDate;
                        response.DispatchState = dto.DispatchState;
                        response.Gender = dto.Gender;
                        response.Address = new Address
                        {
                            Id = dto.AddressId,
                            ZipCode = dto.ZipCode,
                            Street = dto.Street,
                            StreetNumber = dto.StreetNumber,
                            Complement = dto.Complement,
                            City = dto.City,
                            Neighborhood = dto.Neighborhood,
                            State = dto.State,
                        };
                        transaction.Commit();

                        return new Response<ReadCustomerDto>(response, 201, message: "Cliente criado com sucesso");
                    }
                    catch
                    {
                        transaction.Rollback();
                        return new Response<ReadCustomerDto>(null, 500, message: "Erro ao criar cliente");
                    }
                }

            }
        }

        public Response<ReadCustomerDto?> Delete(DeleteCustomerDto dto)
        {
            try
            {
                var dtoRemoved = GetById(new ReadCustomerDto { Id = dto.Id });

                if (dtoRemoved.Data is null) return new Response<ReadCustomerDto?>(null, 404, message: "Cliente não encontrado");

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM Customer WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", dto.Id);
                        command.ExecuteNonQuery();
                    }

                    using (var command = new SqlCommand("DELETE FROM Address WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", dto.AddressId);
                        command.ExecuteNonQuery();
                    }

                }

                return new Response<ReadCustomerDto?>(null, 204, message: $"Cliente {dto.Id} removido com sucesso");
            }
            catch
            {
                return new Response<ReadCustomerDto?>(null, 500, message: "Erro ao remover cliente");
            }
        }

        public Response<List<ReadCustomerDto>> GetAll()
        {
            try
            {
                var customer = new List<ReadCustomerDto>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SELECT c.*, a.* FROM Customer c, Address a " +
                                                            "WHERE c.AddressId = a.Id", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer.Add(new ReadCustomerDto
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Cpf = reader.GetString(reader.GetOrdinal("Cpf")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                                    RG = reader.GetString(reader.GetOrdinal("RG")),
                                    DispatchOrgan = reader.GetString(reader.GetOrdinal("DispatchOrgan")),
                                    DispatchDate = reader.GetDateTime(reader.GetOrdinal("DispatchDate")),
                                    DispatchState = reader.GetString(reader.GetOrdinal("DispatchState")),
                                    Gender = reader.GetString(reader.GetOrdinal("Gender")),
                                    MaritalStatus = reader.GetString(reader.GetOrdinal("MaritalStatus")),
                                    Address = new Address
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("AddressId")),
                                        ZipCode = reader.GetString(reader.GetOrdinal("ZipCode")),
                                        Street = reader.GetString(reader.GetOrdinal("Street")),
                                        StreetNumber = reader.GetString(reader.GetOrdinal("StreetNumber")) ?? string.Empty,
                                        Complement = reader.GetString(reader.GetOrdinal("Complement")) ?? string.Empty,
                                        Neighborhood = reader.GetString(reader.GetOrdinal("Neighborhood")),
                                        City = reader.GetString(reader.GetOrdinal("City")),
                                        State = reader.GetString(reader.GetOrdinal("State")),
                                        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                        ModifiedAt = reader.GetDateTime(reader.GetOrdinal("ModifiedAt")),
                                        ModifiedBy = reader.GetString(reader.GetOrdinal("ModifiedBy")),
                                    },
                                    CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                    ModifiedAt = reader.GetDateTime(reader.GetOrdinal("ModifiedAt")),
                                    ModifiedBy = reader.GetString(reader.GetOrdinal("ModifiedBy")),

                                });
                            }
                        }
                    }
                }

                return new Response<List<ReadCustomerDto>>(customer, message: "Clientes carregados com sucesso");
            }
            catch
            {
                return new Response<List<ReadCustomerDto>>(null, 500, message: "Erro ao carregar clientes");
            }
        }

        public Response<ReadCustomerDto> GetById(ReadCustomerDto dto)
        {
            try
            {
                var customer = new ReadCustomerDto();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SELECT c.*, a.* FROM Customer c, Address a " +
                                                            "WHERE c.AddressId = a.Id AND c.Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", dto.Id);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                customer.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                customer.Cpf = reader.GetString(reader.GetOrdinal("Cpf"));
                                customer.Name = reader.GetString(reader.GetOrdinal("Name"));
                                customer.BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate"));
                                customer.RG = reader.GetString(reader.GetOrdinal("RG"));
                                customer.DispatchOrgan = reader.GetString(reader.GetOrdinal("DispatchOrgan"));
                                customer.DispatchDate = reader.GetDateTime(reader.GetOrdinal("DispatchDate"));
                                customer.DispatchState = reader.GetString(reader.GetOrdinal("DispatchState"));
                                customer.Gender = reader.GetString(reader.GetOrdinal("Gender"));
                                customer.MaritalStatus = reader.GetString(reader.GetOrdinal("MaritalStatus"));
                                customer.Address = new Address
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("AddressId")),
                                    ZipCode = reader.GetString(reader.GetOrdinal("ZipCode")),
                                    Street = reader.GetString(reader.GetOrdinal("Street")),
                                    StreetNumber = reader.GetString(reader.GetOrdinal("StreetNumber")) ?? string.Empty,
                                    Complement = reader.GetString(reader.GetOrdinal("Complement")) ?? string.Empty,
                                    Neighborhood = reader.GetString(reader.GetOrdinal("Neighborhood")),
                                    City = reader.GetString(reader.GetOrdinal("City")),
                                    State = reader.GetString(reader.GetOrdinal("State")),
                                    CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                    ModifiedAt = reader.GetDateTime(reader.GetOrdinal("ModifiedAt")),
                                    ModifiedBy = reader.GetString(reader.GetOrdinal("ModifiedBy")),
                                };
                                customer.CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy"));
                                customer.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                                customer.ModifiedAt = reader.GetDateTime(reader.GetOrdinal("ModifiedAt"));
                                customer.ModifiedBy = reader.GetString(reader.GetOrdinal("ModifiedBy"));

                            }
                        }
                    }
                }

                if(customer.Id <= 0)
                {
                    return new Response<ReadCustomerDto>(null, 404, message: "Cliente não encontrado");

                }

                return new Response<ReadCustomerDto>(customer, message: "Cliente carregado com sucesso");
            }
            catch
            {
                return new Response<ReadCustomerDto>(null, 500, message: "Erro ao carregar cliente");
            }
        }

        public Response<ReadCustomerDto> Update(UpdateCustomerDto dto)
        {

            var response = new ReadCustomerDto();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand("UPDATE Address SET ZipCode = @ZipCode, Street = @Street, StreetNumber = @StreetNumber, " +
                                 "Complement = @Complement, Neighborhood = @Neighborhood, City = @City, State = @State, ModifiedBy = @ModifiedBy, ModifiedAt = @ModifiedAt WHERE Id IN (SELECT AddressId FROM CUSTOMER WHERE Id = @Id )", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", dto.Id);
                            command.Parameters.AddWithValue("@ZipCode", dto.ZipCode);
                            command.Parameters.AddWithValue("@Street", dto.Street);
                            command.Parameters.AddWithValue("@StreetNumber", dto.StreetNumber);
                            command.Parameters.AddWithValue("@Complement", dto.Complement ?? string.Empty);
                            command.Parameters.AddWithValue("@Neighborhood", dto.Neighborhood);
                            command.Parameters.AddWithValue("@City", dto.City);
                            command.Parameters.AddWithValue("@State", dto.State);
                            command.Parameters.AddWithValue("@ModifiedBy", dto.UserId);
                            command.Parameters.AddWithValue("@ModifiedAt", DateTime.Now);

                        }

                        // Inserir o cliente com a foreign key do endereço
                        using (var command = new SqlCommand(
                            "UPDATE Customer SET Cpf = @Cpf, Name = @Name, BirthDate = @BirthDate , RG = @RG , DispatchOrgan = @DispatchOrgan, DispatchDate = @DispatchDate, DispatchState = @DispatchState, Gender = @Gender, MaritalStatus = @MaritalStatus, ModifiedBy = @ModifiedBy, ModifiedAt = @ModifiedAt " +
                            "WHERE Id = @Id ", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", dto.Id);
                            command.Parameters.AddWithValue("@Cpf", dto.Cpf);
                            command.Parameters.AddWithValue("@Name", dto.Name);
                            command.Parameters.AddWithValue("@BirthDate", dto.BirthDate);
                            command.Parameters.AddWithValue("@RG", dto.RG);
                            command.Parameters.AddWithValue("@DispatchOrgan", dto.DispatchOrgan);
                            command.Parameters.AddWithValue("@DispatchDate", dto.DispatchDate);
                            command.Parameters.AddWithValue("@DispatchState", dto.DispatchState);
                            command.Parameters.AddWithValue("@Gender", dto.Gender);
                            command.Parameters.AddWithValue("@MaritalStatus", dto.MaritalStatus);
                            command.Parameters.AddWithValue("@AddressId", dto.AddressId);
                            command.Parameters.AddWithValue("@ModifiedBy", dto.UserId);
                            command.Parameters.AddWithValue("@ModifiedAt", DateTime.Now);

                            command.ExecuteNonQuery();
                        }
                        response.Cpf = dto.Cpf;
                        response.Name = dto.Name;
                        response.BirthDate = dto.BirthDate;
                        response.RG = dto.RG;
                        response.DispatchOrgan = dto.DispatchOrgan;
                        response.DispatchDate = dto.DispatchDate;
                        response.DispatchState = dto.DispatchState;
                        response.Gender = dto.Gender;
                        response.Address = new Address
                        {
                            Id = dto.AddressId,
                            ZipCode = dto.ZipCode,
                            Street = dto.Street,
                            StreetNumber = dto.StreetNumber,
                            Complement = dto.Complement,
                            City = dto.City,
                            Neighborhood = dto.Neighborhood,
                            State = dto.State,
                        };

                        if (dto.Id <= 0)
                        {
                            return new Response<ReadCustomerDto>(null, 404, message: "Cliente não encontrado");

                        }
                        transaction.Commit();

                        return new Response<ReadCustomerDto>(response, 201, message: "Cliente atualizado com sucesso");
                    }
                    catch
                    {
                        transaction.Rollback();
                        return new Response<ReadCustomerDto>(null, 500, message: "Erro ao atualizar cliente");
                    }


                }
            }
        }
    }
}
