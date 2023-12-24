using Newtonsoft.Json;
using ScieenceAPI.Models;
using ScieenceAPI.Models.ForClients;
using Database.Models;

namespace ScieenceAPI.Clients
{
    public class SpringerNatureClient
    {
        private readonly HttpClient _client;
        private static string? _baseUrl;
        private static string? _apiKey;

        public SpringerNatureClient(IConfiguration configuration)
        {
            _baseUrl = configuration["SpringerNature:Url"];
            _apiKey = configuration["SpringerNature:ApiKey"];

            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public async Task<Response> GetPublicationsByKeyword(string keyword, string language, int[] year)
        {
            try
            {

                var response = await ApiDeserialzer($"{keyword} language:{language} datefrom:{year[0]}-01-01 dateto:{year[1]}-12-31");
                return ResponseBeautifier(response);
            }
            catch (Exception ex) {
                return new Response();
            }
        }

        public async Task<Response> GetPublicationsByAuthor(string author, string language, int[] year)
        {
            try
            {
                var response = await ApiDeserialzer($"name:{author} language:{language} datefrom:{year[0]}-01-01 dateto:{year[1]}-12-31");
                return ResponseBeautifier(response);
            }
            catch
            {
                return new Response();
            }
        }
        public async Task<Response> GetPublicationsBySubject(string subject, string language, int[] year)
        {
            try
            {
                var response = await ApiDeserialzer($"subject:{subject} language:{language} datefrom:{year[0]}-01-01 dateto:{year[1]}-12-31");
                return ResponseBeautifier(response);
            }
            catch
            {
                return new Response();
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
                return new Response();
            }
        }

        public async Task<SpringerNaturePub> ApiDeserialzer(string query)
        {
            var s = 1;
            var response = await _client.GetAsync($"/metadata/json?q={query}&s=1&p=200&api_key={_apiKey}");
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
                    PublicationDate = pub.publicationDate,
                    PublicationType = pub.contentType,
                    PublicationYear = Int32.Parse(pub.publicationDate.Remove(4)),
                    Description = pub.Abstract,
                    Doi = pub.identifier.Remove(0, 4),
                };
                if(pub.creators != null) {
                    newPub.Authors = string.Join("; ", pub.creators.ConvertAll(x => x.creator)).Replace(",", "");
                }
                
                if(pub.subjects != null)
                {
                    newPub.Subjects = string.Join("; ", pub.subjects);
                }
                result.Records.Add(newPub);
            }
            return result;
        }
    }
}
