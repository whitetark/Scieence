using Database.Models;
using MongoDB.Driver;

namespace Database.Services
{
    public class PubServices
    {
        private readonly IMongoCollection<Publication> _publications;
        
        public PubServices(DbClient dbClient)
        {
            _publications = dbClient.GetPubsCollection();
        }
        public async Task<List<Publication>> GetPubs()
        {
            try
            {
                 return await _publications.Find(publication => true).ToListAsync();
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Publication> GetPublication(string id)
        {
            try
            {
                return await _publications.Find(publication => publication._id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddPublication(Publication publication)
        {
            try
            {
                await _publications.InsertOneAsync(publication);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DeleteResult> DeletePublication(string id)
        {
            try
            {
               return await _publications.DeleteOneAsync(publication => publication._id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ReplaceOneResult> UpdatePublication(Publication newPublication)
        {
            try
            {
                await GetPublication(newPublication._id);
                return await _publications.ReplaceOneAsync(b => b._id == newPublication._id, newPublication);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}