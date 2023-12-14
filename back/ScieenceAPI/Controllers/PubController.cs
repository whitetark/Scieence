using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ScieenceAPI.Controllers
{
    public class PubController(PublicationServices pubServices) : ControllerBase
    {
        [HttpGet]
        public async Task<DbPublication> GetPublication(string id)
        {
            return await pubServices.GetPublication(id);
        }
        [HttpGet]
        public async Task<List<DbPublication>> GetPublications()
        {
            return await pubServices.GetPublications();
        }
        [HttpDelete]
        public async Task DeletePublication(string id)
        {
            await pubServices.DeletePublication(id);
            return;
        }
        [HttpPut]
        public async Task UpdatePublication(DbPublication publication)
        {
            await pubServices.UpdatePublication(publication);
            return;
        }
        [HttpPost]
        public async Task CreatePublication(DbPublication publication)
        {
            await pubServices.CreatePublication(publication);
            return;
        }
    }
}
