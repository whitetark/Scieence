namespace ScieenceAPI.Clients
{
    public class CoreClient
    {
        private HttpClient _client;
        private static string _baseUrl;
        private static string _apiKey;

        public CoreClient()
        {
            _baseUrl = Config.Core.baseUrl;
            _apiKey = Config.Core.apiKey;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUrl);
        }
    }
}
