using FCC.Teste.Core.Models;
using FCC.Teste.Web.Service.Interfaces;
using System.Text.Json;

namespace FCC.Teste.Web.Service
{
    public class ViaCepService : IViaCepService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ViaCepService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Address?> SearchAddress(string zipcode)
        {

            if (!string.IsNullOrEmpty(zipcode) && zipcode.Length == 8)
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://viacep.com.br/ws/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"{zipcode}/json/");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var address = JsonSerializer.Deserialize<Address>(responseString);

                    return address;
                }
            }

            return null;
        }
    }


}
