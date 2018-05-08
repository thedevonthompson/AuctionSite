using AuctionSite.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AuctionSite.Controllers
{
    [Authorize]
    public class AuctionItemController : Controller
    {
        private ApplicationDbContext db;
        public AuctionItemController()
        {
            db = new ApplicationDbContext();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(AuctionItemDB.GetAllAuctionItems(db));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new AuctionItemCreateViewModel());
        }

        [HttpPost]
        public ActionResult Create(AuctionItemCreateViewModel v)
        {
            if (ModelState.IsValid)
            {
                var a = new AuctionItem(v, CategoryDB.GetCategoryByID(db, v.SelectedCategory), ApplicationUserDB.GetUserByID(db, User.Identity.GetUserId()));
                AuctionItemDB.Create(db, a);
                return RedirectToAction("Index", "AuctionItem");
            }
            return View(v);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                AuctionItem a = AuctionItemDB.GetAuctionItemByID(db, id.Value);
                if (a != null)
                {
                    var v = new AuctionItemCreateViewModel(a);
                    return View(v);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public ActionResult Update(AuctionItemCreateViewModel v)
        {
            if (ModelState.IsValid)
            {
                var a = new AuctionItem(v, CategoryDB.GetCategoryByID(db, v.SelectedCategory), ApplicationUserDB.GetUserByID(db, User.Identity.GetUserId()));
                if (AuctionItemDB.Update(db, a))
                {
                    return RedirectToAction("Index", "AuctionItem");
                }
                ModelState.AddModelError("UpdateFailed", "Currently logged in user is not the owner of this item.");
            }
            return View(v);
        }

    }
}