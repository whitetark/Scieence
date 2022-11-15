using Newtonsoft.Json;
using ScieenceAPI.Models.ForClients;

namespace ScieenceAPI.Clients
{
    public class SpringerNatureClient
    {
        private HttpClient _client;
        private static string _baseUrl;
        private static string _apiKey;

        public SpringerNatureClient()
        {
            _baseUrl = Config.SpringerNature.baseUrl;
            _apiKey = Config.SpringerNature.apiKey;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<SpringerNaturePub> GetPublicationBySomething(string q)
        {
            try
            {
                var response = await _client.GetAsync($"/metadata/json?q={q}&s=1&p=1&api_key={_apiKey}");
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;
                SpringerNaturePub result = JsonConvert.DeserializeObject<SpringerNaturePub>(content);

                return result;

            } catch
            {
                throw new Exception();
            }
        }
    }
}
