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
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class PubController : ControllerBase
    {
        private readonly SpringerNatureClient _springerNatureClient;
        private readonly SemanticScholarClient _semanticScholarClient;
        private readonly AccountServices _accountServices;

        public PubController(SemanticScholarClient semanticScholarClient, SpringerNatureClient springerNatureClient, AccountServices accountServices)
        {
            _semanticScholarClient = semanticScholarClient;
            _springerNatureClient = springerNatureClient;
            _accountServices = accountServices;
        }

        //ApiPub
        [HttpGet("aggregation/getByKeyword/{q}")]
        public async Task<Response> GetPublicationsByKeyword(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsByKeyword(q);
            var sspublications = await _semanticScholarClient.GetPublicationsByKeyword(q);
            //var dbpublications = await _pubServices.GetPublicationsByKeyword(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            //result.Records.AddRange(sspublications.Records);
            //result.Records.AddRange(dbpublications.Records);

            return result;
        }

        [HttpGet("aggregation/getByAuthor/{q}")]
        public async Task<Response> GetPublicationsByAuthor(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsByAuthor(q);
            var sspublications = await _semanticScholarClient.GetPublicationsByAuthor(q);
            //var dbpublications = await _pubServices.GetPublicationsByAuthor(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            //result.Records.AddRange(dbpublications.Records);

            return result;
        }

        [HttpGet("aggregation/getBySubject{q}")]
        public async Task<Response> GetPublicationsBySubject(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsBySubject(q);
            var sspublications = await _semanticScholarClient.GetPublicationsByKeyword(q);
            //var dbpublications = await _pubServices.GetPublicationsBySubject(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            //result.Records.AddRange(dbpublications.Records);

            return result;
        }
        [HttpGet("aggregation/getByLanguage/{q}")]
        public async Task<Response> GetPublicationsByLanguage(string q)
        {
            var snpublications = await _springerNatureClient.GetPublicationsByLanguage(q);
            var sspublications = await _semanticScholarClient.GetPublicationsByLanguage(q);
            //var dbpublications = await _pubServices.GetPublicationsByLanguage(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            //result.Records.AddRange(dbpublications.Records);
            return result;
        }
    }
}