using Database;
using Microsoft.AspNetCore.Mvc;
using ScieenceAPI.Clients;
using ScieenceAPI.Models;

namespace ScieenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PubController : ControllerBase
    {
        private readonly SpringerNatureClient _springerNatureClient;
        private readonly SemanticScholarClient _semanticScholarClient;
        private readonly PubServices _pubServices;

        public PubController(SemanticScholarClient semanticScholarClient, SpringerNatureClient springerNatureClient, PubServices pubServices)
        {
            _semanticScholarClient = semanticScholarClient;
            _springerNatureClient = springerNatureClient;
            _pubServices = pubServices;
        }

        [HttpGet(Name = "GetPubByQ/{q}")]
        public async Task<Response> GetPublicationsByQ(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationBySomething(q, 20);
            var sspublications = await _semanticScholarClient.GetPublicationBySomething(q, 20);
            var tempdbpublications = await GetPublications();

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);

            foreach (var pub in tempdbpublications.Records)
            {
                if (pub.Title.Contains(q) || pub.Description.Contains(q))
                {
                    result.Records.Add(pub);
                }
            }

            return result;
        }
        [HttpGet("getPubs")]
        public async Task<Response> GetPublications()
        {
            var dbpublications = await _pubServices.GetPubs();

            var result = new Response();
            foreach (var pub in dbpublications)
            {
                var newPub = new Record
                {
                    Language = pub.language[0],
                    Url = pub.url,
                    Title = pub.title,
                    Authors = pub.creator,
                    PublicationDate = pub.datePublished,
                    PublicationType = pub.docType,
                    PublicationYear = pub.publicationYear,
                    Doi = (from source in pub.identifier
                          where source.name == "local_doi"
                          select source.value).FirstOrDefault(),
                    Description = pub.description,
                    Subjects = pub.sourceCategory
                };
                result.Records.Add(newPub);
            }
            return result;
        }

        [HttpGet("getPub/{id}")]
        public async Task<DbPub> GetPublication(string id)
        {
            return await _pubServices.GetPublication(id);
        }
        [HttpPost("createPub")]
        public void AddPub(DbPub publication)
        {
            _ = _pubServices.AddPublication(publication);
        }

        [HttpDelete("deletePub/{id}")]
        public void DeletePub(string id)
        {
            _ = _pubServices.DeletePublication(id);
        }

        [HttpPut("updatePub")]
        public void UpdatePub(DbPub publication)
        {
            _ = _pubServices.UpdatePublication(publication);
        }

    }
}