using Database;
using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Mvc;
using ScieenceAPI.Clients;
using ScieenceAPI.Models;

namespace ScieenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScieenceController : ControllerBase
    {
        private readonly SpringerNatureClient _springerNatureClient;
        private readonly SemanticScholarClient _semanticScholarClient;
        private readonly PubServices _pubServices;
        private readonly AccountServices _accountServices;

        public ScieenceController(SemanticScholarClient semanticScholarClient, SpringerNatureClient springerNatureClient, PubServices pubServices, AccountServices accountServices)
        {
            _semanticScholarClient = semanticScholarClient;
            _springerNatureClient = springerNatureClient;
            _pubServices = pubServices;
            _accountServices = accountServices;
        }

        [HttpGet("pub/aggregation/getByKeyword/{q}")]
        public async Task<Response> GetPublicationsByKeyword(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationByKeyword(q, 1000);
            var sspublications = await _semanticScholarClient.GetPublicationByKeyword(q, 1000);
            var tempdbpublications = await GetPublications();

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);

            foreach (var pub in tempdbpublications.Records)
            {
                if (pub.Title.Contains(q, StringComparison.OrdinalIgnoreCase) || pub.Description.Contains(q, StringComparison.OrdinalIgnoreCase))
                {
                    result.Records.Add(pub);
                }
            }

            result.Total = result.Records.Count;

            return result;
        }

        [HttpGet("pub/aggregation/getByAuthor/{q}")]
        public async Task<Response> GetPublicationsByAuthor(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationByKeyword(q, 1000);
            var sspublications = await _semanticScholarClient.GetPublicationByKeyword(q, 1000);
            var tempdbpublications = await GetPublications();

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);

            foreach (var pub in tempdbpublications.Records)
            {
                if (pub.Title.Contains(q, StringComparison.OrdinalIgnoreCase) || pub.Description.Contains(q, StringComparison.OrdinalIgnoreCase))
                {
                    result.Records.Add(pub);
                }
            }

            result.Total = result.Records.Count;

            return result;
        }

        [HttpGet("pub/database/getAll")]
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

        [HttpGet("pub/database/getById/{id}")]
        public async Task<Publication> GetPublication(string id)
        {
            return await _pubServices.GetPublication(id);
        }
        [HttpPost("pub/database/create")]
        public void AddPub(Publication publication)
        {
            _ = _pubServices.AddPublication(publication);
        }

        [HttpDelete("pub/database/deleteById/{id}")]
        public void DeletePub(string id)
        {
            _ = _pubServices.DeletePublication(id);
        }

        [HttpPut("pub/database/update")]
        public void UpdatePub(Publication publication)
        {
            _ = _pubServices.UpdatePublication(publication);
        }

        [HttpGet("acc/database/getById/{id}")]
        public async Task<Account> GetAccount(string id)
        {
            return await _accountServices.GetAccount(id);
        }
        [HttpPost("acc/database/create")]
        public void AddAccount(Account account)
        {
            _ = _accountServices.AddAccount(account);
        }

        [HttpDelete("acc/database/deleteById/{id}")]
        public void DeleteAccount(string id)
        {
            _ = _accountServices.DeleteAccount(id);
        }

        [HttpPut("acc/database/update")]
        public void UpdateAccount(Account account)
        {
            _ = _accountServices.UpdateAccount(account);
        }

    }
}