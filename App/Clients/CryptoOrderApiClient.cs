using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App.Models;
using Newtonsoft.Json;
using System.Net;


namespace App.Clients
{
    public class CryptoOrderApiClient
    {
        public string CryptoOrderApi { get; }
        
        public HttpClient HttpClient { get; }

        public CryptoOrderApiClient(HttpClient httpClient, string cryptoOrderApi)
        {
            if (string.IsNullOrEmpty(cryptoOrderApi))
            {
                throw new System.ArgumentException($"'{nameof(cryptoOrderApi)}' cannot be null or empty", nameof(cryptoOrderApi));
            }

            HttpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            CryptoOrderApi = cryptoOrderApi;
        }

        public async Task<StopLimit> CreateStopLimit(StopLimit model)
        {
            var json = JsonConvert.SerializeObject(model);
            
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{CryptoOrderApi}/stop-limits";

            var httpResponse = await HttpClient.PostAsync(url, data);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<StopLimit>(response);
        }

        public async Task<StopLimit> DeleteStopLimit()
        {
            var url = $"{CryptoOrderApi}/stop-limits/";
            var httpResponse = await HttpClient.DeleteAsync(url);   
            try{
                httpResponse.EnsureSuccessStatusCode();
                var response = await httpResponse.Content.ReadAsStringAsync();
                return null;
            }catch(HttpRequestException ex){
                return null;
            }
        }
        
        public async Task<StopLimit> DeleteStopLimit(StopLimit model)
        {
            var json = JsonConvert.SerializeObject(model);
            
            var url = $"{CryptoOrderApi}/stop-limits/{model.Id}";

            var httpResponse = await HttpClient.DeleteAsync(url);
            
            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<StopLimit>(response);
        }

        public async Task<IReadOnlyCollection<StopLimit>> GetAllStopLimits()
        {
            var url = $"{CryptoOrderApi}/stop-limits";

            var httpResponse = await HttpClient.GetAsync(url);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IReadOnlyCollection<StopLimit>>(response);
        }

    }
}