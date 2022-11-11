namespace ScieenceAPI.Models
{
    public class Publication
    {
        public Results result { get; set; }
        public Records records { get; set; }
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
        public Url url { get; set; }
        public string title { get; set; }
        public Creators[] creators { get; set; }
        public string publicationName { get; set; }
        public bool openaccess { get; set; }
        public string publicationDate { get; set; }
        public string publicationType { get; set; }
        public string genre { get; set; }
    }

    public class Creators
    {
        public string creator { get; set; }
    }

    public class Url
    {
        public string value { get; set; }
    }
}
