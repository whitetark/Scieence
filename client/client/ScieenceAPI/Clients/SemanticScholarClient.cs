using Database.Models;
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

        public async Task<Response> GetPublicationsByKeyword(string q, double numOf)
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
                TotalSSK resultOfDes = JsonConvert.DeserializeObject<TotalSSK>(fullContent);

                var result = new Response();
                foreach (SemanticScholarByKeyword response in resultOfDes.totalContent)
                {
                    foreach (var pub in response.data)
                    {
                        var newPub = new Publication
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

        public async Task<Response> GetPublicationsByAuthor(string author, double numOf)
        {
            var response = await _client.GetAsync($"/graph/v1/author/search?query={author}&fields=papers.title,papers.url,papers.abstract,papers.year,papers.isOpenAccess,papers.fieldsOfStudy,papers.publicationTypes,papers.authors,papers.externalIds,papers.publicationDate&limit={numOf}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            SemanticScholarByAuthor resultOfDes = JsonConvert.DeserializeObject<SemanticScholarByAuthor>(content);

            var result = new Response();
            int num = 0;
            foreach (var data in resultOfDes.data)
            {
                foreach (var pub in data.papers)
                {
                    if (num > numOf)
                    {
                        return result;
                    }
                    num++;

                    var newPub = new Publication
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
        //https://api.semanticscholar.org/graph/v1/paper/search?query=covid+vaccination&offset=100&limit=3&fields=title,url,abstract,year,isOpenAccess,fieldsOfStudy,publicationTypes,authors,externalIds
    }
}
