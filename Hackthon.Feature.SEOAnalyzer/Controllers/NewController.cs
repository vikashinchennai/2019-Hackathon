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
                string str = obj1.ID.ToString();
                SiteContext previewSiteContext = LinkManager.GetPreviewSiteContext(obj1);
                PreviewManager.StoreShellUser(Settings.Preview.AsAnonymous);
                UrlString urlString = new UrlString("/");
                var token = Sitecore.Context.User.GetId().ToString();
                var username = Sitecore.Context.GetUserName();
                urlString["sc_itemid"] = str;
                urlString["sc_mode"] = "preview";
                urlString["sc_lang"] = obj1.Language.ToString();
                urlString["sc_site"] = previewSiteContext.Name;
                WebUtil.SetCookieValue(previewSiteContext.GetCookieKey("sc_date"), string.Empty);
                DeviceSimulationUtil.DeactivateSimulators();
                String test = String.Empty;
                //using (HttpWebResponse response = (HttpWebResponse)Request.ContentEncoding())
                //{
                //    Stream dataStream = response.GetResponseStream();
                //    StreamReader reader = new StreamReader(dataStream);
                //    test = reader.ReadToEnd();
                //    reader.Close();
                //    dataStream.Close();
                //}


            }
        }
 }
    }
