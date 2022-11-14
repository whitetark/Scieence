using MongoDB.Driver;

namespace Database
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
                return await _publications.Find(publication => publication.Id == id).FirstOrDefaultAsync();
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
               return await _publications.DeleteOneAsync(publication => publication.Id == id);
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
                await GetPublication(newPublication.Id);
                return await _publications.ReplaceOneAsync(b => b.Id == newPublication.Id, newPublication);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}