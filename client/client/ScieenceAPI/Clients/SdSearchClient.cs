namespace ScieenceAPI.Clients
{
    public class SdSearchClient
    {
        private HttpClient _client;
        private static string _baseUrl;
        private static string _apiKey;

        public SdSearchClient()
        {
            _baseUrl = Config.SdSearch.baseUrl;
            _apiKey = Config.SdSearch.apiKey;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUrl);
        }
    }
}
