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
            return View();
        }

        [HttpGet]
        public ActionResult CreateOrUpdate(int? id)
        {
            if (id.HasValue)
            {
                return View(AuctionItemDB.GetAuctionItemByID(id.Value));
            }
            return View();
        }

        [HttpPut]
        public ActionResult Create(AuctionItem a)
        {
            if (ModelState.IsValid)
            {
                AuctionItemDB.Create(a);
                return RedirectToAction("Index", "AuctionItem");
            }
            return View(a);
        }

    }
}