using Microsoft.Identity.Client;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Response
    {
        public List<Publication> Records { get; set; }
        public List<KeywordCount> keywordCounts { get; set; }
        public Response()
        {
            Records = new List<Publication>();
            keywordCounts = new List<KeywordCount>();
        }

    }
    public class KeywordCount
    {
        public string Value { get; set; }
        public int Count { get; set; }
        public KeywordCount(string Value, int Count) {
            this.Value = Value;
            this.Count = Count;
        }
    }
    public class Publication
    {
        public string? Language { get; set; }
        public string? Url { get; set; }
        public string? Title { get; set; }
        public string? Doi { get; set; }
        public string? Authors { get; set; }
        public int? PublicationYear { get; set; }
        public string? PublicationDate { get; set; }
        public string? PublicationType { get; set; }
        public string? Description { get; set; }
        public string? Subjects { get; set; }
    }
}
