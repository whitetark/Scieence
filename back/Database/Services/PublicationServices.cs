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
        public async Task<IEnumerable<T>> LoadData<T>(string sql)
        {
            return await _pubDbConnection.QueryAsync<T>(sql);
        }
        public async Task<T> LoadDataSingle<T>(string sql)
        {
            return await _pubDbConnection.QuerySingleAsync<T>(sql);
        }

        public async Task<bool> ExecuteSql(string sql)
        {
            return await _pubDbConnection.ExecuteAsync(sql) > 0;
        }

        public async Task<int> ExecuteSqlWithRowCount(string sql)
        {
            return await _pubDbConnection.ExecuteAsync(sql);
        }

        public async Task<DbPublication> GetPublication(string id)
        {
            string sql = @"SELECT * FROM PublicationSchema.Publications 
            WHERE [PublicationId] = " + id;
            return await LoadDataSingle<DbPublication>(sql);
        }
        public async Task<List<DbPublication>> GetPublications()
        {
            string sql = @"SELECT * FROM PublicationSchema.Publications";
            var publications = await LoadData<DbPublication>(sql);
            List<DbPublication> result = publications.ToList();
            return result;
        }
        public async Task DeletePublication(string id)
        {
            string sql = @"DELETE FROM PublicationSchema.Publications
            WHERE PublicationId = " + id;

            if(await ExecuteSql(sql)){ return; }

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
            "' WHERE PublicationId = " + publication.PublicationId;
    

            if(await ExecuteSql(sql)){ return; }

            throw new Exception("Failed to Update Publication");
        }
        public async Task CreatePublication(DbPublication publication)
        {
            string sql = @"INSERT INTO PublicationSchema.Publications
            ([Description], [Authors], [Subjects],
            [PublicationDate], [PublicationType], [PublicationYear], 
            [Language], [URL], [Title], [DOI]
            ) VALUES (" + "'" + publication.Description +
                "', '" + publication.Authors +
                "', '" + publication.Subjects +
                "', '" + publication.PublicationDate +
                "', '" + publication.PublicationType +
                "', '" + publication.PublicationYear +
                "', '" + publication.Language +
                "', '" + publication.URL +
                "', '" + publication.Title +
                "', '" + publication.DOI + "')";

            if(await ExecuteSql(sql)) {  return; }

            throw new Exception("Failed to Create Publication");
        }
    }
}
