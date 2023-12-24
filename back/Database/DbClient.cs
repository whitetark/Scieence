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
        private readonly IDbConnection _pubDbConnection;
        public DbClient(IOptions<DbConfig> dbConfig)
        {
            _pubDbConnection = new SqlConnection(dbConfig.Value.Pub_Database_Connection);
        }

        public IDbConnection GetPubDbConnection()
        {
            return _pubDbConnection;
        }
    }
}
