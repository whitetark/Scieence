using Database;
using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScieenceAPI.Clients;
using ScieenceAPI.Models;
using System.Data;

namespace ScieenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PubController : ControllerBase
    {
        private readonly SpringerNatureClient _springerNatureClient;
        private readonly SemanticScholarClient _semanticScholarClient;
        private readonly PubServices _pubServices;
        private readonly AccountServices _accountServices;

        public PubController(SemanticScholarClient semanticScholarClient, SpringerNatureClient springerNatureClient, PubServices pubServices, AccountServices accountServices)
        {
            _semanticScholarClient = semanticScholarClient;
            _springerNatureClient = springerNatureClient;
            _pubServices = pubServices;
            _accountServices = accountServices;
        }

        //ApiPub
        [HttpGet("aggregation/getByKeyword/{q}")]
        public async Task<Response> GetPublicationsByKeyword(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsByKeyword(q, 100);
            var sspublications = await _semanticScholarClient.GetPublicationsByKeyword(q, 100);
            var dbpublications = await _pubServices.GetPublicationsByKeyword(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            //result.Records.AddRange(dbpublications.Records);

            return result;
        }

        [HttpGet("aggregation/getByAuthor/{q}")]
        public async Task<Response> GetPublicationsByAuthor(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsByAuthor(q, 100);
            var sspublications = await _semanticScholarClient.GetPublicationsByAuthor(q, 100);
            var dbpublications = await _pubServices.GetPublicationsByAuthor(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            //result.Records.AddRange(dbpublications.Records);

            return result;
        }

        [HttpGet("aggregation/getBySubject{q}")]
        public async Task<Response> GetPublicationsBySubject(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsBySubject(q, 100);
            var sspublications = await _semanticScholarClient.GetPublicationsByKeyword(q, 100);
            var dbpublications = await _pubServices.GetPublicationsBySubject(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            //result.Records.AddRange(dbpublications.Records);

            return result;
        }
        [HttpGet("aggregation/getByLanguage/{q}")]
        public async Task<Response> GetPublicationsByLanguage(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsByLanguage(q, 100);
            var sspublications = await _semanticScholarClient.GetPublicationsByLanguage(q, 100);
            var dbpublications = await _pubServices.GetPublicationsByLanguage(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            //result.Records.AddRange(dbpublications.Records);
            return result;
        }

        // DatabasePub
        [HttpGet("database/getAll")]
        public async Task<List<DbPublication>> GetPublications()
        {
            return await _pubServices.GetPubs();
        }
        [HttpGet("database/getById/{id}")]
        public async Task<DbPublication> GetPublication(string id)
        {
            return await _pubServices.GetPublication(id);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("database/create")]
        public async void AddPub(DbPublication publication)
        {
            await _pubServices.AddPublication(publication);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("database/deleteById/{id}")]
        public async void DeletePub(string id)
        {
            await _pubServices.DeletePublication(id);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("database/update")]
        public async void UpdatePub(DbPublication publication)
        {
            await _pubServices.UpdatePublication(publication);
        }
    }
}