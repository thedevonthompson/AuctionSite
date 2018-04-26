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
        [HttpGet]
        public ActionResult Index()
        {
            return View(AuctionItemDB.GetAllAuctionItems());
        }

        [HttpGet]
        public ActionResult CreateOrUpdate(int? id)
        {
            if (id.HasValue)
            {
                AuctionItem a = AuctionItemDB.GetAuctionItemByID(id.Value);
                if (a != null)
                {
                    return View(a);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(AuctionItem a)
        {
            if (ModelState.IsValid)
            {
                AuctionItemDB.CreateOrUpdate(a);
                return RedirectToAction("Index", "AuctionItem");
            }
            return View(a);
        }

    }
}