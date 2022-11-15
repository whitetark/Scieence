namespace ScieenceAPI.Clients
{
    public class EricClient
    {
        private HttpClient _client;
        private static string _baseUrl;

        public EricClient()
        {
            _baseUrl = Config.Eric.baseUrl;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUrl);
        }
    }
}
