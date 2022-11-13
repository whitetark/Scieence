using Microsoft.AspNetCore.Mvc;
using ScieenceAPI.Clients;
using ScieenceAPI.Models;

namespace ScieenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PubController : ControllerBase
    {
        private readonly PublicApiClient _publicApiClient;
        private readonly ILogger<PubController> _logger;

        public PubController(PublicApiClient publicApiClient, ILogger<PubController> logger)
        {
            _publicApiClient = publicApiClient;
            _logger = logger;
        }

        [HttpGet(Name = "GetPubByQ/{q}")]
        public async Task<Responses> Get(string q)
        {
            var publications = await _publicApiClient.GetPublicationBySomething(q);
            var result = new Responses
            {
                Language = publications.records[0].language,
                Url = publications.records[0].url[0].value,
                Title = publications.records[0].title,
                Creators = publications.records[0].creators.ConvertAll(x => x.creator),
                PublicationName = publications.records[0].publicationName,
                PublicationDate = publications.records[0].publicationDate,
                PublicationType = publications.records[0].publicationType,
                Genre = publications.records[0].genre,
                Description = publications.records[0].Abstract
            };

            return result;
        }
    }
}