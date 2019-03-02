//using Sitecore.Configuration;
//using Sitecore.Data.Items;
//using Sitecore.Links;
//using Sitecore.Publishing;
//using Sitecore.Sites;
//using Sitecore.Text;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace Hackthon.Feature.SEOAnalyzer.Controllers
//{
//    public class SEOController :Controller
//    {
//         public void Index()
//        {
            
//        string RenderingItemID = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";
//                Item obj1 = Sitecore.Context.Database.GetItem(RenderingItemID);
//            if(obj1!=null)
//            {
//                string str = obj1.ID.ToString();
//        SiteContext previewSiteContext = LinkManager.GetPreviewSiteContext(obj1);
//        PreviewManager.StoreShellUser(Settings.Preview.AsAnonymous);
//                UrlString urlString = new UrlString("/");
//        urlString["sc_itemid"] = str;
//                urlString["sc_mode"] = "preview";
//                urlString["sc_lang"] = obj1.Language.ToString();
//                urlString["sc_site"] = previewSiteContext.Name;
//            }

//        }

//        public async Task(JsonObject) GetAsync(string uri)
//{
//    var httpClient = new HttpClient();
//    var response = await httpClient.GetAsync(uri);

//    //will throw an exception if not successful
//    response.EnsureSuccessStatusCode();

//    string content = await response.Content.ReadAsStringAsync();
//    return await Task.Run(() =(JsonObject).Parse(content));
//}
        
//    }
//}