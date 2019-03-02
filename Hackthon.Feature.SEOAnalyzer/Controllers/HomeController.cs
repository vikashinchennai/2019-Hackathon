using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hackthon.Feature.SEOAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public ActionResult MainMenu()
        //{
        //    var currentItem = SitecoreContext.GetCurrentItem<IGlassBase>();
        //    var menuItems = SitecoreContext.Query<Menu_Item>("/sitecore/content/Menu/*[@@templatename='Menu Item']").ToList();

        //    var activeItem = menuItems.AsQueryable().FirstOrDefault(m => m.Link.TargetId == currentItem.Id);
        //    if (activeItem != null)
        //    {
        //        activeItem.IsActive = true;
        //    }

        //    var model = new MainMenu() { MenuItems = menuItems };

        //    return View(model);
        //}

        
    }
}