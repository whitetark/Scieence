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
    }
}
