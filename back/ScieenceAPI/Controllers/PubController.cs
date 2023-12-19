using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ScieenceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PubController(PublicationServices pubServices) : ControllerBase
    {
        [HttpGet]
        [Route("getById/{id}")]
        public async Task<DbPublication> GetPublication(string id)
        {
            return await pubServices.GetPublication(id);
        }
        [HttpGet]
        [Route("getPubs")]
        public async Task<List<DbPublication>> GetPublications()
        {
            return await pubServices.GetPublications();
        }
        [HttpDelete]
        [Route("deleteById/{id}")]
        public async Task DeletePublication(string id)
        {
            await pubServices.DeletePublication(id);
            return;
        }
        [HttpPut]
        [Route("update")]
        public async Task UpdatePublication(DbPublication publication)
        {
            await pubServices.UpdatePublication(publication);
            return;
        }
        [HttpPost]
        [Route("create")]
        public async Task CreatePublication(Publication publication)
        {
            await pubServices.CreatePublication(publication);
            return;
        }
    }
}
