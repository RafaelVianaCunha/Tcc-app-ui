using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App.Models;
using Newtonsoft.Json;

namespace App.Clients
{
    public class CryptoOrderApiClient
    {
        public string cryptoOrderApi { get; }
        
        public HttpClient HttpClient { get; }

        public CryptoOrderApiClient(HttpClient httpClient, string cryptoOrderApi)
        {
            if (string.IsNullOrEmpty(cryptoOrderApi))
            {
                throw new System.ArgumentException($"'{nameof(cryptoOrderApi)}' cannot be null or empty", nameof(cryptoOrderApi));
            }

            HttpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            cryptoOrderApi = cryptoOrderApi;
        }

        public async Task<StopLimit> CreateStopLimit(StopLimit model)
        {
            var json = JsonConvert.SerializeObject(model);
            
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{cryptoOrderApi}/stop-limits";

            var httpResponse = await HttpClient.PostAsync(url, data);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<StopLimit>(response);
        }

        
        public async Task<StopLimit> DeleteStopLimit(StopLimit model)
        {
            var json = JsonConvert.SerializeObject(model);
            
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{cryptoOrderApi}/stop-limits/{moodel.Id}";

            var httpResponse = await HttpClient.DeleteAsync(url, data);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<StopLimit>(response);
        }

        public async Task<StopLimit> GetAllStopLimits(StopLimit model)
        {
            var url = $"{cryptoOrderApi}/saleorder";

            var httpResponse = await HttpClient.GetAsync(url);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IReadOnlyCollection<SaleOrder>>(response);
        }

    }
}