using Newtonsoft.Json;

namespace ScieenceAPI.Models.ForClients
{
    public class SemanticScholarByAuthor
    {
        public List<DataA> data { get; set; }
    }
    public class DataA
    {
        public List<Paper> papers {get; set; }
    }
    public class Paper
    {
        public ExternalIds externalIds { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        [JsonProperty("abstract")]
        public string Abstract { get; set; }
        public int? year { get; set; }
        public bool isOpenAccess { get; set; }
        public List<string> fieldsOfStudy { get; set; }
        public List<string> publicationTypes { get; set; }
        public string publicationDate { get; set; }
        public List<Authors> authors { get; set; }
    }
}

