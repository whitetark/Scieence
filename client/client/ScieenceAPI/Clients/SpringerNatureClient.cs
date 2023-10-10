using Database.Models;
using Newtonsoft.Json;
using ScieenceAPI.Models;
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

        public async Task<Response> GetPublicationsByKeyword(string keyword, double numOf)
        {
            try
            {
                var response = await ApiDeserialzer(keyword, numOf);
                return await ResponseBeautifier(response);
            } 
            catch
            {
                throw new Exception();
            }
        }
        public async Task<Response> GetPublicationsByAuthor(string author, double numOf)
        {
            try
            {
                var response = await ApiDeserialzer("name:" + author, numOf);
                return await ResponseBeautifier(response);
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task<Response> GetPublicationsBySubject(string subject, double numOf)
        {
            try
            {
                var response = await ApiDeserialzer("subject:" + subject, numOf);
                return await ResponseBeautifier(response);
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task<Response> GetPublicationsByLanguage(string language, double numOf)
        {
            try
            {
                var response = await ApiDeserialzer("language:"+language, numOf);
                return await ResponseBeautifier(response);
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<TotalSN> ApiDeserialzer(string query, double numOf)
        {
            var s = 1;
            string fullContent = "";
            for (int i = 0; i < Math.Ceiling(numOf / 50); i++)
            {
                var response = await _client.GetAsync($"/metadata/json?q={query}&s={s}&p=50&api_key={_apiKey}");
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;

                fullContent = fullContent + "," + content;

                s += 50;
            }
            fullContent = "{\"totalContent\":[" + fullContent.Remove(0, 1) + "]}";
            return JsonConvert.DeserializeObject<TotalSN>(fullContent);
        }
        public async Task<Response> ResponseBeautifier(TotalSN total)
        {
            var result = new Response();
            foreach (var response in total.totalContent)
            {
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
            }
            return result;
        }
    }
}
