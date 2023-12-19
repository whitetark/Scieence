using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Azure;
using Dapper;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    public class PublicationServices(DbClient dbClient)
    {
        private readonly IDbConnection _pubDbConnection = dbClient.GetPubDbConnection();
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

        public async Task<DbPublication> GetPublication(string id)
        {
            try
            {
                string sql = @"SELECT * FROM PublicationSchema.Publications 
                WHERE PublicationId = @id";
                return await _pubDbConnection.QuerySingleAsync<DbPublication>(sql, new { id });
            } catch
            {
                throw new Exception("Failed to Get Publication");
            }

        }
        public async Task<List<DbPublication>> GetPublications()
        {
            try
            {
                string sql = @"SELECT * FROM PublicationSchema.Publications";
                var publications = await _pubDbConnection.QueryAsync<DbPublication>(sql);
                List<DbPublication> result = publications.ToList();
                return result;
            }
            catch
            {
                throw new Exception("Failed to Get Publications");
            }
        }
        public async Task DeletePublication(string id)
        {
            string sql = @"DELETE FROM PublicationSchema.Publications
            WHERE PublicationId = @id";

            if(await _pubDbConnection.ExecuteAsync(sql, new { id }) > 0){ return; }

            throw new Exception("Failed to Delete Publication");
        }
        public async Task UpdatePublication(DbPublication publication)
        {
            string sql = @"UPDATE PublicationSchema.Publications 
            SET [Description] = '"+ publication.Description + 
            "', [Authors] = '"+ publication.Authors + 
            "', [Subjects] = '"+ publication.Subjects + 
            "', [PublicationDate] = '" + publication.PublicationDate +
            "', [PublicationType] = '" + publication.PublicationType +
            "', [PublicationYear] = '" + publication.PublicationYear +
            "', [Language] = '" + publication.Language + 
            "', [URL] = '" + publication.URL +
            "', [Title] = '" + publication.Title +
            "', [DOI] = '" + publication.DOI + 
            "' WHERE PublicationId = @PublicationId";
    

            if(await _pubDbConnection.ExecuteAsync(sql, new { publication.PublicationId }) > 0){ return; }

            throw new Exception("Failed to Update Publication");
        }
        public async Task<DbPublication> CreatePublication(Publication publication)
        {
            try
            {
                string sql = @"SELECT * FROM PublicationSchema.Publications 
                WHERE DOI = @Doi";

                var test = await _pubDbConnection.QuerySingleAsync<DbPublication>(sql, new { publication.Doi });
                return test;
            }
            catch(Exception)
            {
                string sql = @"INSERT INTO PublicationSchema.Publications
                ([Description], [Authors], [Subjects],
                [PublicationDate], [PublicationType], [PublicationYear], 
                [Language], [URL], [Title], [DOI]) 
                OUTPUT INSERTED.*
                VALUES (" + "'" + publication.Description +
                       "', '" + publication.Authors +
                       "', '" + publication.Subjects +
                       "', '" + publication.PublicationDate +
                       "', '" + publication.PublicationType +
                       "', '" + publication.PublicationYear +
                       "', '" + publication.Language +
                       "', '" + publication.Url +
                       "', '" + publication.Title +
                       "', '" + publication.Doi + "')";

                return await _pubDbConnection.QuerySingleAsync<DbPublication>(sql);
            }
        }

        public async Task<Models.Response> GetPublicationsByKeyword(string query)
        {
            string sql = @"SELECT * FROM PublicationSchema.Publications
            WHERE Title LIKE @query OR Description LIKE @query";

            var publications = await _pubDbConnection.QueryAsync<DbPublication>(sql, new { query = "%" + query + "%" });
            List<DbPublication> pubList = publications.ToList();

            var result = new Models.Response();

            foreach (var pub in pubList)
            {
                var newPub = new Publication 
                { 
                    Language = pub.Language, 
                    Url = pub.URL, 
                    Title = pub.Title, 
                    Authors = pub.Authors, 
                    PublicationDate = pub.PublicationDate, 
                    PublicationType = pub.PublicationType, 
                    PublicationYear = pub.PublicationYear, 
                    Description = pub.Description, 
                    Doi = pub.DOI, 
                    Subjects = pub.Subjects, 
                };

                result.Records.Add(newPub);
            }
            return result;
        }
    }
}
