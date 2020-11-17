using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App.Models;
using Newtonsoft.Json;

namespace App.Clients
{
    public class ExchangeCredentialClient
    {
        public string ExchangeApi { get; }
        
        public HttpClient HttpClient { get; }

        public ExchangeCredentialClient(HttpClient httpClient, string exchangeApi)
        {
            if (string.IsNullOrEmpty(exchangeApi))
            {
                throw new System.ArgumentException($"'{nameof(exchangeApi)}' cannot be null or empty", nameof(exchangeApi));
            }

            HttpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            ExchangeApi = exchangeApi;
        }

        public async Task<ExchangeCredentialModel> CreateCredential(ExchangeCredentialModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{ExchangeApi}/exchange-credentials";

            var httpResponse = await HttpClient.PostAsync(url, data);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ExchangeCredentialModel>(response);
        }
    }
}