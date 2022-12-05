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

        //ApiPub
        [HttpGet("pub/aggregation/getByKeyword/{q}")]
        public async Task<Response> GetPublicationsByKeyword(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsByKeyword(q, 100);
            var sspublications = await _semanticScholarClient.GetPublicationsByKeyword(q, 100);
            var dbpublications = await _pubServices.GetPublicationsByKeyword(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            result.Records.AddRange(dbpublications.Records);

            result.Total = result.Records.Count;

            return result;
        }

        [HttpGet("pub/aggregation/getByAuthor/{q}")]
        public async Task<Response> GetPublicationsByAuthor(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsByAuthor(q, 100);
            var sspublications = await _semanticScholarClient.GetPublicationsByAuthor(q, 100);
            var dbpublications = await _pubServices.GetPublicationsByAuthor(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            result.Records.AddRange(dbpublications.Records);

            result.Total = result.Records.Count;

            return result;
        }

        [HttpGet("pub/aggregation/getBySubject{q}")]
        public async Task<Response> GetPublicationsBySubject(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsBySubject(q, 1000);
            var sspublications = await _semanticScholarClient.GetPublicationsByKeyword(q, 1000);
            var dbpublications = await _pubServices.GetPublicationsBySubject(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            result.Records.AddRange(dbpublications.Records);

            result.Total = result.Records.Count;

            return result;
        }
        [HttpGet("pub/aggregation/getByLanguage/{q}")]
        public async Task<Response> GetPublicationsByLanguage(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsByLanguage(q, 1000);
            var sspublications = await _semanticScholarClient.GetPublicationsByKeyword(q, 1000);
            var dbpublications = await _pubServices.GetPublicationsByLanguage(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            result.Records.AddRange(dbpublications.Records);

            result.Total = result.Records.Count;

            return result;
        }

        // DatabasePub
        [HttpGet("pub/database/getAll")]
        public async Task<List<DbPublication>> GetPublications()
        {
            return await _pubServices.GetPubs();
        }
        [HttpGet("pub/database/getById/{id}")]
        public async Task<DbPublication> GetPublication(string id)
        {
            return await _pubServices.GetPublication(id);
        }
        [HttpPost("pub/database/create")]
        public async void AddPub(DbPublication publication)
        {
            _ = _pubServices.AddPublication(publication);
        }
        [HttpDelete("pub/database/deleteById/{id}")]
        public async void DeletePub(string id)
        {
            _ = await _pubServices.DeletePublication(id);
        }

        [HttpPut("pub/database/update")]
        public async void UpdatePub(DbPublication publication)
        {
            _ = await _pubServices.UpdatePublication(publication);
        }


        // DatabaseAcc
        [HttpGet("acc/database/getById/{id}")]
        public async Task<Account> GetAccount(string id)
        {
            return await _accountServices.GetAccount(id);
        }
        [HttpPost("acc/database/create")]
        public async void AddAccount(Account account)
        {
            _ = _accountServices.AddAccount(account);
        }

        [HttpDelete("acc/database/deleteById/{id}")]
        public async void DeleteAccount(string id)
        {
            _ = await _accountServices.DeleteAccount(id);
        }

        [HttpPut("acc/database/update")]
        public async void UpdateAccount(Account account)
        {
            _ = await _accountServices.UpdateAccount(account);
        }
    }
}