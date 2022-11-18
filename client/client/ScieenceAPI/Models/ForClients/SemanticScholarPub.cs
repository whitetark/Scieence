using Newtonsoft.Json;

namespace ScieenceAPI.Models.ForClients
{
    public class TotalSS
    {
        public List<SemanticScholarPub> totalContent { get; set; }
    }
    
    public class SemanticScholarPub
    {
        public List<Data> data { get; set; }
        public SemanticScholarPub()
        {
            data = new List<Data>();
        }
    }
    public class Data
    {
        public ExternalIds externalIds { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        [JsonProperty("abstract")]
        public string Abstract { get; set; }
        public int year { get; set; }
        public bool isOpenAccess { get; set; }
        public List<string> fieldsOfStudy { get; set; }
        public List<string> publicationTypes { get; set; }
        public string publicationDate { get; set; }
        public List<Authors> authors { get; set; }
    }
    public class Authors
    {
        public string name { get; set; }
    }
    public class ExternalIds
    {
        public string DOI { get; set; }
    }
    /*
     * {
    "total": 1029389,
    "offset": 100,
    "next": 103,
    "data": [
        {
            "paperId": "744a690f6d64b6823947925315c0d83df46a1d30",
            "externalIds": {
                "PubMedCentral": "8327820",
                "DOI": "10.1136/bcr-2021-244125",
                "CorpusId": 236519556,
                "PubMed": "34330729"
            },
            "url": "https://www.semanticscholar.org/paper/744a690f6d64b6823947925315c0d83df46a1d30",
            "title": "Guillain-Barré syndrome after COVID-19 vaccination",
            "abstract": "We report a case of Guillain-Barré syndrome (GBS) occurring soon after the first dose of Vaxzevria (previously known as COVID-19 vaccine AstraZeneca). Thus far, there has been no evidence of an increased risk of GBS resulting from either COVID-19 infection nor from COVID-19 vaccines; however, individual cases and population cohorts should be scrutinised, in order to ensure the constant evaluation of such risks. It is as yet not possible to draw conclusions about any significant association between COVID-19 vaccination and GBS. A temporal correlation does not imply, and should not be deemed to signify, causality. However, it is important to remain vigilant, so that any potential increased risk is properly evaluated. The specific presentation of bifacial weakness as the initial symptom may be a characteristic feature of GBS in the context of recent COVID-19 vaccination.",
            "year": 2021,
            "isOpenAccess": true,
            "fieldsOfStudy": [
                "Medicine"
            ],
            "publicationTypes": [
                "JournalArticle"
            ],
            "authors": [
                {
                    "authorId": "2121507464",
                    "name": "Norma McKean"
                },
                {
                    "authorId": "15185646",
                    "name": "Charmaine Chircop"
                }
            ]
        },
    */
}
