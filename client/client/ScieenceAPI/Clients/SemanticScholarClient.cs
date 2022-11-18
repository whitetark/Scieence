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

        public async Task<Response> GetPublicationBySomething(string q, int numOf)
        {
            try
            {
                //q = q.Replace(" ", "+");
                var response = await _client.GetAsync($"/graph/v1/paper/search?query={q}&offset=100&limit={numOf}&fields=title,url,abstract,year,isOpenAccess,fieldsOfStudy,publicationTypes,authors,externalIds,publicationDate");
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;
                SemanticScholarPub resultOfDes = JsonConvert.DeserializeObject<SemanticScholarPub>(content);

                var result = new Response();
                foreach (var pub in resultOfDes.data)
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
                        pub.publicationTypes = new List<string>() {"Article"};
                    }
                    newPub.PublicationType = pub.publicationTypes[0];
                    result.Records.Add(newPub);
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
