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
    public class PubController(SemanticScholarClient semanticScholarClient, SpringerNatureClient springerNatureClient, PublicationServices pubServices) : ControllerBase
    {

        //ApiPub
        [HttpGet("aggregation/getByKeyword/{q}")]
        public async Task<Response> GetPublicationsByKeyword(string q)
        {
            var snpublications = await springerNatureClient.GetPublicationsByKeyword(q);
            var sspublications = await semanticScholarClient.GetPublicationsByKeyword(q);
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
            var snpublications = await springerNatureClient.GetPublicationsByAuthor(q);
            var sspublications = await semanticScholarClient.GetPublicationsByAuthor(q);
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
            var snpublications = await springerNatureClient.GetPublicationsBySubject(q);
            var sspublications = await semanticScholarClient.GetPublicationsByKeyword(q);
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
            var snpublications = await springerNatureClient.GetPublicationsByLanguage(q);
            var sspublications = await semanticScholarClient.GetPublicationsByLanguage(q);
            //var dbpublications = await _pubServices.GetPublicationsByLanguage(q);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            //result.Records.AddRange(dbpublications.Records);
            return result;
        }
    }
}