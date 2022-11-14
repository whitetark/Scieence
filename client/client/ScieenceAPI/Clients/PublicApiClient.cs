using Newtonsoft.Json;
using ScieenceAPI.Models;

namespace ScieenceAPI.Clients
{
    public class PublicApiClient
    {
        private HttpClient _client;
        private static string _baseUrl;
        private static string _apiKey;

        public PublicApiClient()
        {
            _baseUrl = "http://api.springernature.com";
            _apiKey = "eb9ca71479a5ebe84088018aad8881a2";

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<TempPublication> GetPublicationBySomething(string q)
        {
            try
            {
                var response = await _client.GetAsync($"/metadata/json?q={q}&s=1&p=1&api_key={_apiKey}");
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;
                TempPublication result = JsonConvert.DeserializeObject<TempPublication>(content);

                return result;

            } catch
            {
                throw new Exception();
            }
        }
    }
}
