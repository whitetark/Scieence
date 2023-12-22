using Database.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Database
{
    public class DbClient
    {
        private readonly IMongoCollection<Account> _accounts;
        private readonly IDbConnection _pubDbConnection;
        public DbClient(IOptions<DbConfig> dbConfig)
        {
            var client = new MongoClient(dbConfig.Value.Connection_String);
            _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
            var database = client.GetDatabase(dbConfig.Value.User_Database_Name);
            _accounts = database.GetCollection<Account>(dbConfig.Value.Accounts_Collection_Name);
        }

        public IDbConnection GetPubDbConnection()
        {
            return _pubDbConnection;
        }

        public IMongoCollection<Account> GetAccountCollection()
        {
            return _accounts;
        }
    }
}
