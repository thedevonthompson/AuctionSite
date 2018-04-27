using AuctionSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuctionSite.Controllers
{
    public class AuctionItemController : Controller
    {
        private static ApplicationDbContext db;
        static AuctionItemController()
        {
            db = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(AuctionItemDB.GetAllAuctionItems(db));
        }

        [HttpGet]
        public ActionResult CreateOrUpdate(int? id)
        {
            if (id.HasValue)
            {
                AuctionItem a = AuctionItemDB.GetAuctionItemByID(db, id.Value);
                if (a != null)
                {
                    ViewData["CreateOrUpdateText"] = "Update";
                    var v = new AuctionItemCreateViewModel(a);
                    return View(v);
                }
            }
            ViewData["CreateOrUpdateText"] = "Create";
            return View(new AuctionItemCreateViewModel());
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(AuctionItemCreateViewModel v)
        {
            if (ModelState.IsValid)
            {
                var a = new AuctionItem(v, CategoryDB.GetCategoryByID(db, v.SelectedCategory));
                AuctionItemDB.CreateOrUpdate(db, a);
                return RedirectToAction("Index", "AuctionItem");
            }
            return View(v);
        }

    }
}