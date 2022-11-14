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
        private readonly PublicApiClient _publicApiClient;
        private readonly PubServices _pubServices;

        public PubController(PublicApiClient publicApiClient, PubServices pubServices)
        {
            _publicApiClient = publicApiClient;
            _pubServices = pubServices;
        }

        [HttpGet(Name = "GetPubByQ/{q}")]
        public async Task<Publication> Get(string q)
        {
            var publications = await _publicApiClient.GetPublicationBySomething(q);
            var result = new Publication
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

            AddPub(result);

            return result;
        }
        [HttpGet("getPubs")]
        public async Task<List<Publication>> GetPublications()
        {
            return await _pubServices.GetPubs();
        }

        [HttpGet("getPub/{id}")]
        public async Task<Publication> GetPublication(string id)
        {
            return await _pubServices.GetPublication(id);
        }
        [HttpPost("createPub")]
        public void AddPub(Publication publication)
        {
            _ = _pubServices.AddPublication(publication);
        }

        [HttpDelete("deletePub/{id}")]
        public void DeletePub(string id)
        {
            _ = _pubServices.DeletePublication(id);
        }

        [HttpPut("updatePub")]
        public void UpdatePub(Publication publication)
        {
            _ = _pubServices.UpdatePublication(publication);
        }

    }
}