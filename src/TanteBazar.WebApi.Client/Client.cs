using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TanteBazar.WebApi.Client
{
    public class Client
    {
        private readonly Configuration _config;
        private HttpClient _httpClient;

        public Client(Configuration configuration)
        {
            _config = configuration;

            _httpClient = new HttpClient();
        }

        public async Task<List<Models.Item>> GetItemsAsync()
        {
            _httpClient.DefaultRequestHeaders.Add("X_API_SECRET", _config.ClientSecret);

            var resultString = await _httpClient.GetStringAsync($"{_config.ServiceUrl}/items");

            if (string.IsNullOrEmpty(resultString))
                return null;

            var result = JsonConvert.DeserializeObject<List<Models.Item>>(resultString);

            return result;
    }
}
