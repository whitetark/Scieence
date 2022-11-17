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
        /* "contentType":"Article",
         "url":[
            {
               "value":"http://dx.doi.org/10.1186/s12888-014-0370-0"
            }
         ],
         "title":"Differentiating ADHD from oral language difficulties in children: role of movements and effects of stimulant medication",
         "creators":[
            {
               "creator":"Hughes, Carroll W"
            },
            {
               "creator":"Pickering, Joyce"
            },
            {
               "creator":"Baker, Kristi"
            },
            {
               "creator":"Bolanos, Gina"
            },
            {
               "creator":"Silver, Cheryl"
            }
         ],
         "publicationName":"BMC Psychiatry",
         "issn":"1471-244X",
         "openaccess":"true",
         "publicationDate":"2014-12-31",
         "genre":"OriginalPaper",
         "abstract":"AbstractBackgroundThe current study was designed to test if an objective measure of both attention and movement would differentiate children with Oral Language Disorders (OLD) from those with comorbid Attention Deficit/Hyperactivity Disorder (ADHD) and if stimulant medication improved performance when both disorders were present.MethodsThe sample consisted of thirty-three children with an identified oral language disorder (of which 22 had comorbid ADHD) ages 6 to 13 who were enrolled in a yearlong intensive learning intervention program. Those on a stimulant medication were tested at baseline and again a year later on and off medication.ResultsObjective measures that included an infrared motion analysis system which tracked and recorded subtle movements discriminated children with OLD from those with a comorbid ADHD disorder whereas classic attention measures did not. There were better attention scores and fewer movements in children while on-medication.ConclusionsUse of an objective measurement that includes movement detection improves objective diagnostic differential for OLD and ADHD and provides quantifiable changes in performance related to medication for both OLD and ADHD."
        */
        public class SemanticScholar
        {
            public static string baseUrl = "https://api.semanticscholar.org";

            //https://api.semanticscholar.org/api-docs/graph
        }
    }
}
