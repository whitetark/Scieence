using Newtonsoft.Json;
using ScieenceAPI.Models;
using ScieenceAPI.Models.ForClients;

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

        public async Task<Response> GetPublicationBySomething(string q, double numOf)
        {
            try
            {
                var s = 1;
                string fullContent = "";
                for (int i = 0; i < Math.Ceiling(numOf / 100); i++)
                {
                    var response = await _client.GetAsync($"/graph/v1/paper/search?query={q}&offset={s}&limit=100&fields=title,url,abstract,year,isOpenAccess,fieldsOfStudy,publicationTypes,authors,externalIds,publicationDate");
                    response.EnsureSuccessStatusCode();
                    var content = response.Content.ReadAsStringAsync().Result;

                    fullContent = fullContent + "," + content.Remove(content.Length - 1);

                    s += 100;
                }

                fullContent = "{\"totalContent\":[" + fullContent.Remove(0,1) + "]}\n";

                TotalSS resultOfDes = JsonConvert.DeserializeObject<TotalSS>(fullContent);

                var result = new Response();
                foreach (SemanticScholarPub response in resultOfDes.totalContent)
                {
                    foreach (var pub in response.data)
                    {
                        var newPub = new Record
                        {
                            Language = "en",
                            Url = pub.url,
                            Title = pub.title,
                            Authors = pub.authors.ConvertAll(x => x.name),
                            PublicationDate = pub.publicationDate,
                            PublicationYear = pub.year,
                            Description = pub.Abstract,
                            Doi = pub.externalIds.DOI,
                            Subjects = pub.fieldsOfStudy
                        };
                        if (pub.publicationTypes == null)
                        {
                            pub.publicationTypes = new List<string>() { "Article" };
                        }
                        newPub.PublicationType = pub.publicationTypes[0];
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
        //https://api.semanticscholar.org/graph/v1/paper/search?query=covid+vaccination&offset=100&limit=3&fields=title,url,abstract,year,isOpenAccess,fieldsOfStudy,publicationTypes,authors,externalIds
    }
}
