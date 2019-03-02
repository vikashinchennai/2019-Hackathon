using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackthon.Foundation.Common.Models.ContentGenerator
{
    public class ContentExtractorRequest
    {
        public string Url { get; set; }
        public Dictionary<string,string> CookieContent { get; set; }
        public Uri Domain { get; set; }
    }
}