using Newtonsoft.Json;
using ScieenceAPI.Models;
using ScieenceAPI.Models.ForClients;

namespace ScieenceAPI.Clients
{
    public class SpringerNatureClient
    {
        private readonly HttpClient _client;
        private static string? _baseUrl;
        private static string? _apiKey;

        public SpringerNatureClient()
        {
            _baseUrl = Config.SpringerNature.baseUrl;
            _apiKey = Config.SpringerNature.apiKey;

            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public async Task<Response> GetPublicationsByKeyword(string keyword)
        {
            try
            {
                var response = await ApiDeserialzer(keyword);
                return ResponseBeautifier(response);
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task<Response> GetPublicationsByAuthor(string author)
        {
            try
            {
                var response = await ApiDeserialzer("name:" + author);
                return ResponseBeautifier(response);
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task<Response> GetPublicationsBySubject(string subject)
        {
            try
            {
                var response = await ApiDeserialzer("subject:" + subject);
                return ResponseBeautifier(response);
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task<Response> GetPublicationsByLanguage(string language)
        {
            try
            {
                var response = await ApiDeserialzer("language:" + language);
                return ResponseBeautifier(response);
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<SpringerNaturePub> ApiDeserialzer(string query)
        {
            var s = 1;
            var response = await _client.GetAsync($"/metadata/json?q={query}&s={s}&p=20&api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<SpringerNaturePub>(content);
        }
        public Response ResponseBeautifier(SpringerNaturePub response)
        {
            var result = new Response();

            foreach (var pub in response.records)
            {
                var newPub = new Publication
                {
                    Language = pub.language,
                    Url = pub.url[0].value,
                    Title = pub.title,
                    Authors = pub.creators.ConvertAll(x => x.creator),
                    PublicationDate = pub.publicationDate,
                    PublicationType = pub.contentType,
                    PublicationYear = Int32.Parse(pub.publicationDate.Remove(4)),
                    Description = pub.Abstract,
                    Doi = pub.identifier.Remove(0, 4),
                    Subjects = pub.subjects
                };
                result.Records.Add(newPub);
            }
            return result;
        }
    }
}
