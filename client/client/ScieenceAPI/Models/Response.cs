namespace ScieenceAPI.Models
{
    public class Response
    {
        public List<Record> Records { get; set; }
        public Response()
        {
            Records = new List<Record>();
        }
    }
    public class Record
    {
        public string Language { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Doi { get; set; }
        public List<string> Authors { get; set; }
        public int PublicationYear { get; set; }
        public string PublicationDate { get; set; }
        public string PublicationType { get; set; }
        public string Description { get; set; }
        public List<string> Subjects { get; set; }
    }
}
