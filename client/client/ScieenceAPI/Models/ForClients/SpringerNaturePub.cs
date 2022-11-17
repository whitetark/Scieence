using Newtonsoft.Json;

namespace ScieenceAPI.Models.ForClients
{
    public class SpringerNaturePub
    {
        public List<Records> records { get; set; }
    }
    public class Records
    {
        public string contentType { get; set; }
        public string identifier { get; set; }
        public string language { get; set; }
        public List<Url> url { get; set; }
        public string title { get; set; }
        public List<Cretors> creators { get; set; }
        public string openaccess { get; set; }
        public string publicationDate { get; set; }
        public string publicationType { get; set; }
        [JsonProperty("abstract")]
        public string Abstract { get; set; }
        public List<string> subjects { get; set; }
    }

    public class Cretors
    {
        public string creator { get; set; }
    }

    public class Url
    {
        public string value { get; set; }
    }
    /*
     * "records": [
        {
            "contentType": "Article",
            "identifier": "doi:10.1007/s41095-022-0275-7",
            "language": "en",
            "url": [
                {
                    "format": "",
                    "platform": "",
                    "value": "http://dx.doi.org/10.1007/s41095-022-0275-7"
                }
            ],
            "title": "A survey of urban visual analytics: Advances and future directions",
            "creators": [
                {
                    "creator": "Deng, Zikun"
                },
                {
                    "creator": "Weng, Di"
                },
                {
                    "creator": "Liu, Shuhan"
                },
                {
                    "creator": "Tian, Yuan"
                },
                {
                    "creator": "Xu, Mingliang"
                },
                {
                    "creator": "Wu, Yingcai"
                }
            ],
            "publicationName": "Computational Visual Media",
            "openaccess": "true",
            "doi": "10.1007/s41095-022-0275-7",
            "publisher": "Springer",
            "publicationDate": "2023-03-01",
            "publicationType": "Journal",
            "issn": "2096-0662",
            "volume": "9",
            "number": "1",
            "genre": [
                "ReviewPaper",
                "Review Article"
            ],
            "startingPage": "3",
            "endingPage": "39",
            "journalId": "41095",
            "copyright": "©2022 The Author(s)",
            "abstract": "Developing effective visual analytics systems demands care in characterization of domain problems and integration of visualization techniques and computational models. Urban visual analytics has already achieved remarkable success in tackling urban problems and providing fundamental services for smart cities. To promote further academic research and assist the development of industrial urban analytics systems, we comprehensively review urban visual analytics studies from four perspectives. In particular, we identify 8 urban domains and 22 types of popular visualization, analyze 7 types of computational method, and categorize existing systems into 4 types based on their integration of visualization techniques and computational models. We conclude with potential research directions and opportunities.",
            "subjects": [
                "Computer Science",
                "Computer Graphics",
                "User Interfaces and Human Computer Interaction",
                "Artificial Intelligence",
                "Image Processing and Computer Vision"
            ]
        }
    ],
    */
}
