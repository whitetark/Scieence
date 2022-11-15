namespace ScieenceAPI
{
    public class Config
    {
        public class SpringerNature
        {
            public static string baseUrl = "http://api.springernature.com";
            public static string apiKey = "eb9ca71479a5ebe84088018aad8881a2";

            //https://dev.springernature.com/example-metadata-response
        }
        public class SdSearch
        {
            public static string baseUrl = "https://api.elsevier.com";
            public static string apiKey = "d4b8fe6e8a132098cc67e13f1eff3f02";

            //https://dev.elsevier.com/tecdoc_sdsearch_migration.html
        }
        public class Core
        {
            public static string baseUrl = "https://api.core.ac.uk";
            public static string apiKey = "utmVOs1l5USLkJGifbd7ERvDXQFpegCo";

            //https://api.core.ac.uk/docs/v3#section/Welcome!
        }
        public class Doaj
        {
            public static string baseUrl = "https://doaj.org/api";

            //https://doaj.org/api/docs#!/Search/get_api_search_journals_search_query
        }
        public class Eric
        {
            public static string baseUrl = "https://api.ies.ed.gov";

            //https://eric.ed.gov/?api#/default/get_eric_ no urls
        }
        public class SemanticScholar
        {
            public static string baseUrl = "https://api.semanticscholar.org";

            //https://api.semanticscholar.org/api-docs/graph
        }
    }
}
