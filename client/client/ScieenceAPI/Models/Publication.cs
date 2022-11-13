using Newtonsoft.Json;

namespace ScieenceAPI.Models
{
    public class Publication
    {
        public List<Results> result { get; set; }
        public List<Records> records { get; set; }
    }

    public class Results
    {
        public int total { get; set; }
        public int start { get; set; }
    }

    public class Records
    {
        public string contentType { get; set; }
        public string language { get; set; }
        public List<Url> url { get; set; }
        public string title { get; set; }
        public List<Cretors> creators { get; set; }
        public string publicationName { get; set; }
        public string publicationDate { get; set; }
        public string publicationType { get; set; }
        public string genre { get; set; }
        [JsonProperty("abstract")]
        public string Abstract { get; set; }
    }

    public class Cretors
    {
        public string creator { get; set; }
    }

    public class Url
    {
        public string value { get; set; }
    }
}
