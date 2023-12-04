using Database.Models;
using Newtonsoft.Json;
using ScieenceAPI.Models;
using ScieenceAPI.Models.ForClients;

namespace ScieenceAPI.Clients
{

    public class SemanticScholarClient
    {
        private readonly HttpClient _client;
        private static string? _baseUrl;

        public SemanticScholarClient()
        {
            _baseUrl = Config.SemanticScholar.baseUrl;

            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public async Task<Response> GetPublicationsByKeyword(string q)
        {
            try
            {
                var s = 1;

                var response = await _client.GetAsync($"/graph/v1/paper/search?query={q}&offset={s}&limit=20&fields=title,url,abstract,year,isOpenAccess,fieldsOfStudy,publicationTypes,authors,externalIds,publicationDate");
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;

                var resultOfDes = JsonConvert.DeserializeObject<SemanticScholarByKeyword>(content);

                var result = new Response();

                foreach (var pub in resultOfDes.data)
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
                    pub.publicationTypes ??= ["Article"];
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

        public async Task<Response> GetPublicationsByLanguage(string language)
        {
            var result = new Response();
            if (language == "en")
            {
                Random rnd = new();
                char randomChar = (char)rnd.Next('a', 'z');
                var publications = await GetPublicationsByKeyword(randomChar.ToString());
                result.Records.AddRange(publications.Records);
            }
            return result;
        }

        public async Task<Response> GetPublicationsByAuthor(string author)
        {
            var response = await _client.GetAsync($"/graph/v1/author/search?query={author}&fields=papers.title,papers.url,papers.abstract,papers.year,papers.isOpenAccess,papers.fieldsOfStudy,papers.publicationTypes,papers.authors,papers.externalIds,papers.publicationDate&limit=20");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            SemanticScholarByAuthor resultOfDes = JsonConvert.DeserializeObject<SemanticScholarByAuthor>(content);

            var result = new Response();
            foreach (var data in resultOfDes.data)
            {
                foreach (var pub in data.papers)
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
                    pub.publicationTypes ??= ["Article"];
                    newPub.PublicationType = pub.publicationTypes[0];
                    result.Records.Add(newPub);
                }
            }
            return result;
        }
        //https://api.semanticscholar.org/graph/v1/paper/search?query=covid+vaccination&offset=100&limit=3&fields=title,url,abstract,year,isOpenAccess,fieldsOfStudy,publicationTypes,authors,externalIds
    }
}
