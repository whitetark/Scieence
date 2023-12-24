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
        public int PublicationId { get; set; }
        public string? Description { get; set; }
        public string? Authors { get; set; }
        public string? Subjects { get; set; }
        public string? PublicationDate { get; set; }
        public string? PublicationType { get; set; }
        public int? PublicationYear { get; set; }
        public string? Language { get; set; }
        public string? URL { get; set; }
        public string? Title { get; set; }
        public string? DOI { get; set; }
    }
}
