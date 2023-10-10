using Database.Models;
using MongoDB.Driver;

namespace Database.Services
{
    public class PubServices
    {
        private readonly IMongoCollection<DbPublication> _publications;

        public PubServices(DbClient dbClient)
        {
            _publications = dbClient.GetPubsCollection();
        }
        public async Task<List<DbPublication>> GetPubs()
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
        public async Task<DbPublication> GetPublication(string id)
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
        public async Task<Response> GetPublicationsByKeyword(string keyword)
        {
            try
            {
                var publications = await GetPubs();
                var result = new List<DbPublication>();
                foreach(var publication in publications)
                {
                    if(publication.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) || publication.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)){
                        result.Add(publication);
                    }
                }

                return await ResponseBeautifier(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> GetPublicationsByAuthor(string author)
        {
            try
            {
                var publications = await GetPubs();
                var result = new List<DbPublication>();
                foreach (var publication in publications)
                {
                    var authors = string.Join(" ", publication.Authors);
                    if (authors.Contains(author, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(publication);
                    }
                }
                return await ResponseBeautifier(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> GetPublicationsByLanguage(string language)
        {
            try
            {
                var publications = await _publications.Find(publication => publication.Language == language).ToListAsync();
                return await ResponseBeautifier(publications);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> GetPublicationsBySubject(string subject)
        {
            try
            {
                var publications = await _publications.Find(publication => publication.Subjects.Contains(subject)).ToListAsync();
                return await ResponseBeautifier(publications);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddPublication(DbPublication publication)
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
        public async Task<ReplaceOneResult> UpdatePublication(DbPublication newPublication)
        {
            try
            {
                await GetPublication(newPublication._id);
                return await _publications.ReplaceOneAsync(b => b._id == newPublication._id, newPublication);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> ResponseBeautifier(List<DbPublication> dbPubs)
        {
            var result = new Response();
            foreach (var pub in dbPubs)
            {
                var newPub = new Publication
                {
                    Language = pub.Language,
                    Url = pub.Url,
                    Title = pub.Title,
                    Authors = pub.Authors,
                    PublicationDate = pub.PublicationDate,
                    PublicationType = pub.PublicationType,
                    PublicationYear = pub.PublicationYear,
                    Doi = pub.Doi,
                    Description = pub.Description,
                    Subjects = pub.Subjects
                };
                result.Records.Add(newPub);
            }
            return result;
        }
    }
}