using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class DbPublication
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        public string Description { get; set; }
        public List<string> Authors { get; set; }
        public string PublicationDate { get; set; }
        public string PublicationType { get; set; }
        public string Language { get; set; }
        public int? PublicationYear { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Doi { get; set; }
        public List<string> Subjects { get; set; }
    }
}
