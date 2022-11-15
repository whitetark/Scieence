namespace ScieenceAPI.Clients
{
    public class SemanticScholarClient
    {
        private HttpClient _client;
        private static string _baseUrl;

        public SemanticScholarClient()
        {
            _baseUrl = Config.SemanticScholar.baseUrl;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUrl);
        }
    }
}
