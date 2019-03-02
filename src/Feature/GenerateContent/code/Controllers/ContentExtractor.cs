using Hackthon.Feature.SEOAnalyzer.ExtensionMethods;
using Hackthon.Feature.SEOAnalyzer.Models;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Publishing;
using Sitecore.Security.Accounts;
using Sitecore.Shell.DeviceSimulation;
using Sitecore.Sites;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Sitecore.Security.Authentication;
using System.Web;
using System.IO;
using Hackthon.Foundation.Common.Models.ContentGenerator;
namespace GenerateContent.Controllers
{
    public class ContentExtractor
    {
       
        public void process()
        {
            //PipeLine Method, need to map arguments

            var request = new ContentExtractorRequest();
            var cookieCollection = BuildCookies(request.CookieContent, request.Domain));
            var response = MakeAiCall(request.Url, cookieCollection);

            return response;

            //TODO: Need to map resonse to pieline for next action

        }

        public CookieCollection BuildCookies(Dictionary<string, string> cookieDictonary, string domain)
        {
            CookieCollection cookieCollection = new CookieCollection();
            domain = Convert.ToString(domain);
            foreach (var eachCookie in cookieDictonary)
            {
                cookieCollection.Add(new Cookie(eachCookie.Key, eachCookie.Value, "", domain));
            }
            return cookieCollection;
        }

        public string MakeAiCall(string url, CookieCollection cookies=null)
        {
            using (HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url))
            {
                request.Method = "GET";
                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer = Cookies;
                }
                String htmlBody = String.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    htmlBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }
                return htmlBody;
            }
        }

    }
}