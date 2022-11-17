using MongoDB.Driver;

namespace Database
{
    public class PubServices
    {
        private readonly IMongoCollection<DbPub> _publications;
        
        public PubServices(DbClient dbClient)
        {
            _publications = dbClient.GetPubsCollection();
        }
        public async Task<List<DbPub>> GetPubs()
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
        public async Task<DbPub> GetPublication(string id)
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
        public async Task AddPublication(DbPub publication)
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
        public async Task<ReplaceOneResult> UpdatePublication(DbPub newPublication)
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