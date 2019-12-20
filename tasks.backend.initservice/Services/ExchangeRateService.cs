using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using tasks.backend.initservice.Models;

namespace tasks.backend.initservice.Services
{
    public interface IExchangeRate
    {
        Task<Rate> GetRate();
    }
    public class ExchangeRateService : IExchangeRate
    {
        private IConfiguration _configuration;
        private HttpClient _httpClient;

        public ExchangeRateService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient("namedClient");
        }
        public async Task<Rate> GetRate()
        {
            var currency = GetCurrency();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://api.exchangeratesapi.io/latest?symbols={currency}"),
                Method = HttpMethod.Get,
            };
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Rate>(responseContent);
                data.Identifier = System.Guid.NewGuid().ToString();
                return data;
            }
            return null;
        }
        private string GetCurrency()
        {
            var random = new Random();
            List<string> currencies = new List<string>();
            currencies.Add("GBP");
            currencies.Add("INR");
            currencies.Add("USD");
            currencies.Add("CNY");
            currencies.Add("CAD");
            currencies.Add("AUD");
            currencies.Add("MXN");
            currencies.Add("CHF");
            int index = random.Next(currencies.Count);
            return currencies[index];
        }
    }
}
