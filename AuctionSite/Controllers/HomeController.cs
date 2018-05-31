using AuctionSite.Models;
using AuctionSite.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuctionSite.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db;
        public HomeController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            List<AuctionItem> items = AuctionItemDB.GetFeaturedAuctionItems(db);

            foreach (var item in items)
            {
                if (item.Images.Count < 1)
                {
                    item.Images.Add(ItemImage.DefaultImage());
                }
            }

            return View(items);
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
    }
}