using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Publication
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        public string description { get; set; }
        public List<string> creator { get; set; }
        public string datePublished { get; set; }
        public string docType { get; set; }
        public List<Identifier> identifier { get; set; }
        public List<string> language { get; set; }
        public int publicationYear { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public List<string> sourceCategory { get; set; }
    }
    public class Identifier
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
