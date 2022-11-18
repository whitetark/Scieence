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

        public async Task<Response> GetPublicationBySomething(string q, double numOf)
        {
            try
            {
                var s = 1;
                string fullContent = "";
                for (int i = 0; i < Math.Ceiling(numOf / 50); i++)
                {
                    var response = await _client.GetAsync($"/metadata/json?q={q}&s={s}&p=50&api_key={_apiKey}");
                    response.EnsureSuccessStatusCode();
                    var content = response.Content.ReadAsStringAsync().Result;

                    fullContent = fullContent + "," + content;

                    s += 50;
                }

                fullContent = "{\"totalContent\":[" + fullContent.Remove(0, 1) + "]}";

                TotalSN resultOfDes = JsonConvert.DeserializeObject<TotalSN>(fullContent);

                var result = new Response();
                foreach (var response in resultOfDes.totalContent)
                {
                    foreach (var pub in response.records)
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
                            Doi = pub.identifier.Remove(0, 4),
                            Subjects = pub.subjects
                        };
                        result.Records.Add(newPub);
                    }
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
