using AuctionSite.Models;
using AuctionSite.Models.Database;
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
            List<AuctionItem> items = AuctionItemDB.GetAllAuctionItems(db);

            foreach (var item in items)
            {
                if (item.Images.Count < 1)
                {
                    item.Images.Add(ItemImage.DefaultImage());
                }
            }

            return View(items);
        }

        [HttpPost]
        public ActionResult Bid(int? AuctionItemID, decimal amount)
        {
            if (AuctionItemID.HasValue)
            {
                AuctionItem a = AuctionItemDB.GetAuctionItemByID(db, AuctionItemID.Value);
                if (a != null)
                {
                    if (a.User.Id == User.Identity.GetUserId() || amount < a.MinPrice)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    BidDB.Create(db, new Bid(User.Identity.GetUserId(), AuctionItemID.Value, amount));
                    return RedirectToAction("Details", new { id = a.AuctionItemID });
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new AuctionItemCreateViewModel());
        }

        [HttpPost]
        public ActionResult Create(AuctionItemCreateViewModel v)
        {
            DateTime minDate = DateTime.Now;
            DateTime maxDate = minDate.AddDays(7);

            bool isEndDateValid = v.EndDateTime > minDate && v.EndDateTime < maxDate;

            if (ModelState.IsValid && isEndDateValid)
            {
                List<ItemImage> images = new List<ItemImage>();

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];

                    if (file.ContentLength > 0 && file.ContentLength <= 4194304 && 
                        ( file.ContentType == "image/gif" || file.ContentType == "image/jpeg" || file.ContentType == "image/png") )
                    {
                        images.Add(new ItemImage(BlobStorageHelper.UploadBlob(User.Identity.GetUserId(), Guid.NewGuid().ToString(), file)));
                    }
                }

                var a = new AuctionItem(v, CategoryDB.GetCategoryByID(db, v.SelectedCategory), ApplicationUserDB.GetUserByID(db, User.Identity.GetUserId()), images);
                AuctionItemDB.Create(db, a);
                return RedirectToAction("Index", "AuctionItem");
            }

            if (!isEndDateValid)
            {
                ModelState.AddModelError("EndDateTime", $"Auction item end time must be between {minDate} and {maxDate}");
            }

            return View(v);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                AuctionItem a = AuctionItemDB.GetAuctionItemByID(db, id.Value);
                if (a != null)
                {
                    return View(a);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
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

        [HttpGet]
        public ActionResult ViewBids()
        {
            return View(BidDB.GetBidsByUserID(db, User.Identity.GetUserId()));
        }

    }
}