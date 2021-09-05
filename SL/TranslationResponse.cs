using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL
{
    internal class TranslationResponse
    {
        public class Rootobject
        {
            public Responsedata responseData { get; set; }
            public bool quotaFinished { get; set; }
            public object mtLangSupported { get; set; }
            public string responseDetails { get; set; }
            public int responseStatus { get; set; }
            public string responderId { get; set; }
            public object exception_code { get; set; }
            public Match[] matches { get; set; }
        }

        public class Responsedata
        {
            public string translatedText { get; set; }
            public float match { get; set; }
        }

        public class Match
        {
            public string id { get; set; }
            public string segment { get; set; }
            public string translation { get; set; }
            public string source { get; set; }
            public string target { get; set; }
            public string quality { get; set; }
            public object reference { get; set; }
            public int usagecount { get; set; }
            public string subject { get; set; }
            public string createdby { get; set; }
            public string lastupdatedby { get; set; }
            public string createdate { get; set; }
            public string lastupdatedate { get; set; }
            public float match { get; set; }
        }
    }
}
