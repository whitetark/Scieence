using Azure.Core;
using Azure.Core.GeoJson;
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
    public class AggregationController(SemanticScholarClient semanticScholarClient, SpringerNatureClient springerNatureClient, PublicationServices pubServices) : ControllerBase
    {

        //ApiPub
        [HttpGet("getByKeyword")]
        public async Task<Response> GetPublicationsByKeyword([FromQuery(Name = "query")] string query)
        {
            //var snpublications = await springerNatureClient.GetPublicationsByKeyword(query);
            //var sspublications = await semanticScholarClient.GetPublicationsByKeyword(query);
            var dbpublications = await pubServices.GetPublicationsByKeyword(query);

            var result = new Response();

            //result.Records.AddRange(snpublications.Records);
            //result.Records.AddRange(sspublications.Records);
            result.Records.AddRange(dbpublications.Records);
            
            FilterListByDOI(result);
            
            return result;
        }

        [HttpGet("getByAuthor")]
        public async Task<Response> GetPublicationsByAuthor([FromQuery(Name = "query")] string query)
        {
            var snpublications = await springerNatureClient.GetPublicationsByAuthor(query);
            var sspublications = await semanticScholarClient.GetPublicationsByAuthor(query);
            var dbpublications = await pubServices.GetPublicationsByAuthor(query);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            result.Records.AddRange(dbpublications.Records);

            FilterListByDOI(result);

            return result;
        }

        [HttpGet("getBySubject")]
        public async Task<Response> GetPublicationsBySubject([FromQuery(Name = "query")] string query)
        {
            var snpublications = await springerNatureClient.GetPublicationsBySubject(query);
            var sspublications = await semanticScholarClient.GetPublicationsByKeyword(query);
            var dbpublications = await pubServices.GetPublicationsBySubject(query);

            var result = new Response();

            result.Records.AddRange(snpublications.Records);
            result.Records.AddRange(sspublications.Records);
            result.Records.AddRange(dbpublications.Records);

            FilterListByDOI(result);

            return result;
        }

        public static Response FilterListByDOI(Response response)
        {
            var result = new Response();
            result.Records = response.Records.DistinctBy(x => x.Doi).ToList();
            return result;
        }
    }
}