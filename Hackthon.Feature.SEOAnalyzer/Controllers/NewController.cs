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

namespace Hackthon.Feature.SEOAnalyzer.Controllers
{
    public class NewController : Controller
    {


        public void Index()
        {
            string RenderingItemID = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";

            Item obj1 = Sitecore.Context.Database.GetItem(RenderingItemID);
            if (obj1 != null)
            {
                string url = string.Empty;
                string str = obj1.ID.ToString();
                SiteContext previewSiteContext = LinkManager.GetPreviewSiteContext(obj1);
                PreviewManager.StoreShellUser(Settings.Preview.AsAnonymous);
                UrlString urlString = new UrlString("/");
                var token = Sitecore.Context.User.GetId().ToString();
                var username = Sitecore.Context.GetUserName();
                //  request.IsSecureConnection
                url = "https://";
                //  else
                url = "http://";

                urlString["sc_itemid"] = str;
                urlString["sc_mode"] = "preview";
                urlString["sc_lang"] = obj1.Language.ToString();
                urlString["sc_site"] = previewSiteContext.Name;
                WebUtil.SetCookieValue(previewSiteContext.GetCookieKey("sc_date"), string.Empty);
                DeviceSimulationUtil.DeactivateSimulators();
                ProcessMethod(urlString.ToString());
                Dictionary<string, string> dict = new Dictionary<string, string>();
                var userticket = GetIndividualCookies("sitecore_userticket");
                var AspNetCookie = GetIndividualCookies(".AspNet.Cookies");
                dict.Add(sitecore_userticket, userticket);
                dict.Add(AspNetCookie, AspNetCookie);
            }
        }
        public string GetIndividualCookies(string cookieKey)
        {
            return HttpRequest().Cookies[cookieKey].Value;
        }


        public void ProcessMethod(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            String test = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                test = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }

        }






    }
}