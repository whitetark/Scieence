using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    public class PublicationServices(DbClient dbClient)
    {
        private readonly IDbConnection _pubDbConnection = dbClient.GetPubDbConnection();
        public IEnumerable<T> Loadata<T>(string sql)
        {
            return _pubDbConnection.Query<T>(sql);
        }
        public T LoadDataSingle<T>(string sql)
        {
            return _pubDbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            return _pubDbConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            return _pubDbConnection.Execute(sql);
        }
    }
}
