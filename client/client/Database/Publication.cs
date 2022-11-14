using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Publication
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Language { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public List<string> Creators { get; set; }
        public string PublicationName { get; set; }
        public string PublicationDate { get; set; }
        public string PublicationType { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public List<string> Keywords { get; set; }
        public List<string> Subjects { get; set; }
    }
}
