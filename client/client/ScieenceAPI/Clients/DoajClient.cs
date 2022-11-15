namespace ScieenceAPI.Clients
{
    public class DoajClient
    {
        private HttpClient _client;
        private static string _baseUrl;

        public DoajClient()
        {
            _baseUrl = Config.Doaj.baseUrl;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUrl);
        }
    }
}
