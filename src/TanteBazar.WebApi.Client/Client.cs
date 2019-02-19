using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TanteBazar.WebApi.Client.Models;

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
            

            var resultString = "";

            using (var httpResponse = await _httpClient.GetAsync($"{_config.ServiceUrl}/api/items"))
            {
                resultString = await httpResponse.Content.ReadAsStringAsync();
            }

                if (string.IsNullOrEmpty(resultString))
                    return null;

            var result = JsonConvert.DeserializeObject<List<Models.Item>>(resultString);

            return result;
        }

        public async Task<Basket> GetBasketAsync()
        {
            _httpClient.DefaultRequestHeaders.Add("X_API_SECRET", _config.ClientSecret);


            var resultString = "";

            using (var httpResponse = await _httpClient.GetAsync($"{_config.ServiceUrl}/api/basket"))
            {
                resultString = await httpResponse.Content.ReadAsStringAsync();
            }

            if (string.IsNullOrEmpty(resultString))
                return null;

            var result = JsonConvert.DeserializeObject<Basket>(resultString);

            return result;
        }

        public async Task AddItemToBasket(BasketItemRequest item)
        {
            _httpClient.DefaultRequestHeaders.Add("X_API_SECRET", _config.ClientSecret);

            var httpRequest = new HttpRequestMessage();
            httpRequest.Method = HttpMethod.Post;
            httpRequest.RequestUri = new Uri($"{_config.ServiceUrl}/api/basket");
            httpRequest.Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            using (var httpResponse = await _httpClient.SendAsync(httpRequest))
            {
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"adding item to basket was not successful. Error Code: {httpResponse.StatusCode}");
                }
            }
        } 
    }
}
