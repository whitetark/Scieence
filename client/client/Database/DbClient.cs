﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DbClient
    {
        private readonly IMongoCollection<Publication> _publications;
        public DbClient(IOptions<DbConfig> publicationsDbConfig)
        {
            var client = new MongoClient(publicationsDbConfig.Value.Connection_String);
            var database = client.GetDatabase(publicationsDbConfig.Value.Database_Name);
            _publications = database.GetCollection<Publication>(publicationsDbConfig.Value.Publications_Collection_Name);
        }
        public IMongoCollection<Publication> GetPubsCollection()
        {
            return _publications;
        }
    }
}