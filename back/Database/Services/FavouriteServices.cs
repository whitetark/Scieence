using Dapper;
using Database.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    public class FavouriteServices(IOptions<DbConfig> dbConfig)
    {

        //public async Task<IEnumerable<T>> LoadData<T>(string sql)
        //{
        //    return await _pubDbConnection.QueryAsync<T>(sql);
        //}
        //public async Task<T> LoadDataSingle<T>(string sql)
        //{
        //    return await _pubDbConnection.QuerySingleAsync<T>(sql);
        //}

        //public async Task<bool> ExecuteSql(string sql)
        //{
        //    return await _pubDbConnection.ExecuteAsync(sql) > 0;
        //}

        //public async Task<int> ExecuteSqlWithRowCount(string sql)
        //{
        //    return await _pubDbConnection.ExecuteAsync(sql);
        //}

        public async Task<List<DbPublication>> GetFavoritesByUsername(string username)
        {
            try
            {
                string sql = @"SELECT P.*
                FROM PublicationSchema.Accounts AS A
                INNER JOIN PublicationSchema.Favourites AS F
                ON A.AccountId = F.AccountId
                INNER JOIN PublicationSchema.Publications AS P
                ON F.PublicationId = P.PublicationId
                WHERE A.Username = @username";
                var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
                var publications = await _pubDbConnection.QueryAsync<DbPublication>(sql, new { username });
                if(publications.Count() == 0 )
                {
                    return new List<DbPublication>();
                }

                List<DbPublication> result = publications.ToList();
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("", ex);
            }
        }
        public async Task<bool> AddFavorite(int accountId, int publicationId)
        {
            try
            {
                string sql = @"SELECT * FROM PublicationSchema.Favourites
                WHERE (AccountId = @accountId) AND (PublicationId = @publicationId)";
                var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
                var test = await _pubDbConnection.QueryAsync<DbPublication>(sql, new { accountId, publicationId });

                if (test.Count() > 0)
                {
                    return false;
                }

                sql = @"INSERT INTO PublicationSchema.Favourites
                ([AccountId],[PublicationId]) 
                VALUES (@accountId, @publicationId)";

                return await _pubDbConnection.ExecuteAsync(sql, new
                {
                    accountId,
                    publicationId
                }) > 0;
            }
            catch (Exception)
            {
                throw new Exception("Failed to Add Favorite");
            }
        }
        public async Task DeleteFavorite(int accountId, int publicationId)
        {
            string sql = @"DELETE FROM PublicationSchema.Favourites
            WHERE AccountId = @accountId AND PublicationId = @publicationId";
            var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
            if (await _pubDbConnection.ExecuteAsync(sql, new { accountId, publicationId }) > 0) { return; }

            throw new Exception("Failed to Delete Favourite");
        }
    }
}
