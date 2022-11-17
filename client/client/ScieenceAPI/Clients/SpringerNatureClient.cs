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

        public async Task<Response> GetPublicationBySomething(string q, int numOf)
        {
            try
            {
                var response = await _client.GetAsync($"/metadata/json?q={q}&s=1&p={numOf}&api_key={_apiKey}");
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;
                SpringerNaturePub resultOfDes = JsonConvert.DeserializeObject<SpringerNaturePub>(content);

                var result = new Response();
                foreach (var pub in resultOfDes.records)
                {
                    var newPub = new Record
                    {
                        Language = pub.language,
                        Url = pub.url[0].value,
                        Title = pub.title,
                        Authors = pub.creators.ConvertAll(x => x.creator),
                        PublicationDate = pub.publicationDate,
                        PublicationType = pub.contentType,
                        PublicationYear = Int32.Parse(pub.publicationDate.Remove(4)),
                        Description = pub.Abstract,
                        Doi = pub.identifier,
                        Subjects = pub.subjects
                    };
                    result.Records.Add(newPub);
                }
                return result;
            } 
            catch
            {
                throw new Exception();
            }
        }
    }
}
