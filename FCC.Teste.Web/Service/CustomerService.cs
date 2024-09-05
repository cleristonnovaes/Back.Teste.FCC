using FCC.Teste.Core.DTOs.Customers;
using FCC.Teste.Core.Response;
using FCC.Teste.Web.Service.Interfaces;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace FCC.Teste.Web.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CustomerResponseList> GetAll(string token)
        {
            var client = _httpClientFactory.CreateClient("CustomerApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("api/customer");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<CustomerResponseList>(content);

            if (response.IsSuccessStatusCode)
                return res;

            return new CustomerResponseList();
        }

        public async Task<UpdateCustomerDto> GetById(int id, string token)
        {
            var client = _httpClientFactory.CreateClient("CustomerApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"api/customer/{id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<CustomerResponse>(content);

            if (response.IsSuccessStatusCode)
            {
                var updateCustomer = new UpdateCustomerDto
                {
                    Id = res.User.Data.Id,
                    Cpf = res.User.Data.Cpf,
                    Name = res.User.Data.Name,
                    BirthDate = res.User.Data.BirthDate,
                    RG = res.User.Data.Rg,
                    DispatchOrgan = res.User.Data.DispatchOrgan,
                    DispatchDate = res.User.Data.DispatchDate,
                    DispatchState = res.User.Data.DispatchState,
                    Gender = res.User.Data.Gender,
                    MaritalStatus = res.User.Data.MaritalStatus,
                    ZipCode = res.User.Data.Address.Cep,
                    Street = res.User.Data.Address.Logradouro,
                    StreetNumber = res.User.Data.Address.StreetNumber,
                    Complement = res.User.Data.Address.Complement,
                    Neighborhood = res.User.Data.Address.Bairro,
                    City = res.User.Data.Address.Localidade,
                    State = res.User.Data.Address.Uf
                };
                return updateCustomer;
            }
            return new UpdateCustomerDto();

        }
        public async Task<bool> Create(CreateCustomerDto dto, string token)
        {
            var client = _httpClientFactory.CreateClient("CustomerApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("api/customer", dto);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> Delete(DeleteCustomerDto dto, string token)
        {
            var client = _httpClientFactory.CreateClient("CustomerApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonPayload = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"api/customer", UriKind.Relative),
                Content = content
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> Update(UpdateCustomerDto dto, string token)
        {
            var client = _httpClientFactory.CreateClient("CustomerApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsJsonAsync("api/customer", dto);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
