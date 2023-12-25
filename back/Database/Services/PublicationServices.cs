using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Azure;
using Dapper;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    public class PublicationServices(IOptions<DbConfig> dbConfig)
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

        public async Task<DbPublication> GetPublication(string id)
        {
            try
            {
                string sql = @"SELECT * FROM PublicationSchema.Publications 
                WHERE PublicationId = @id";
                var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
                return await _pubDbConnection.QuerySingleAsync<DbPublication>(sql, new { id });
            } catch
            {
                throw new Exception("Failed to Get Publication");
            }
        }
        public async Task<DbPublication> GetPublicationByUrl(string url)
        {
            try
            {
                string sql = @"SELECT * FROM PublicationSchema.Publications 
                WHERE URL = @url";
                var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
                return await _pubDbConnection.QuerySingleAsync<DbPublication>(sql, new { url });
            }
            catch
            {
                throw new Exception("Failed to Get Publication");
            }

        }
        public async Task<List<DbPublication>> GetPublications()
        {
            try
            {
                string sql = @"SELECT * FROM PublicationSchema.Publications";
                var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
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
            var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
            if (await _pubDbConnection.ExecuteAsync(sql, new { id }) > 0){ return; }

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

            var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
            if (await _pubDbConnection.ExecuteAsync(sql, new { publication.PublicationId }) > 0){ return; }

            throw new Exception("Failed to Update Publication");
        }
        public async Task<DbPublication> CreatePublication(Publication publication)
        {
            try
            {
                string sql = @"SELECT * FROM PublicationSchema.Publications 
                WHERE URL = @Url";
                var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
                var test = await _pubDbConnection.QueryAsync<DbPublication>(sql, new { publication.Url });

                if (test.Count() > 0)
                {
                    return test.FirstOrDefault();
                }

                sql = @"INSERT INTO PublicationSchema.Publications
                ([Description], [Authors], [Subjects],
                [PublicationDate], [PublicationType], [PublicationYear], 
                [Language], [URL], [Title], [DOI]) 
                OUTPUT INSERTED.*
                VALUES (@description, @authors, @subjects, @publicationDate,
                @publicationType, @publicationYear, @language, @url, @title, @doi)";

                return await _pubDbConnection.QuerySingleAsync<DbPublication>(sql, new {
                    description = publication.Description,
                    authors = publication.Authors,
                    subjects = publication.Subjects,
                    publicationDate = publication.PublicationDate,
                    publicationType = publication.PublicationType,
                    publicationYear = publication.PublicationYear,
                    language = publication.Language,
                    url = publication.Url,
                    title = publication.Title,
                    doi = publication.Doi,
                });
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        public async Task<Models.Response> GetPublicationsByKeyword(string query, string language, int[] year)
        {
            string sql = @"SELECT * FROM PublicationSchema.Publications
            WHERE (Title LIKE @query OR Description LIKE @query) AND (Language LIKE @language)
            AND (PublicationYear BETWEEN @year1 AND @year2)";
            var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
            var publications = await _pubDbConnection.QueryAsync<DbPublication>(sql, new { query = "%" + query + "%", language, year1 = year[0], year2 = year[1] });
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

        public async Task<Models.Response> GetPublicationsByAuthor(string query, string language, int[] year)
        {
            string sql = @"SELECT * FROM PublicationSchema.Publications
            WHERE (Authors LIKE @query) AND (Language LIKE @language)
            AND (PublicationYear BETWEEN @year1 AND @year2)";

            var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
            var publications = await _pubDbConnection.QueryAsync<DbPublication>(sql, new { query = "%" + query + "%", language, year1 = year[0], year2 = year[1] });
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

        public async Task<Models.Response> GetPublicationsBySubject(string query, string language, int[] year)
        {
            string sql = @"SELECT * FROM PublicationSchema.Publications
            WHERE (Subjects LIKE @query) AND (Language LIKE @language)
            AND (PublicationYear BETWEEN @year1 AND @year2)";

            var _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
            var publications = await _pubDbConnection.QueryAsync<DbPublication>(sql, new { query = "%" + query + "%", language, year1 = year[0], year2 = year[1] });
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
