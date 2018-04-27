using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AuctionSite.Models
{
    public class AuctionItemDB
    {

        public static void Create(ApplicationDbContext db, AuctionItem a)
        {
            db.AuctionItems.Add(a);
            db.SaveChanges();
        }

        public static List<AuctionItem> GetAllAuctionItems(ApplicationDbContext db)
        {
            return db.AuctionItems.Include("Category").ToList();
        }

        public static void CreateOrUpdate(ApplicationDbContext db, AuctionItem a)
        {
            if (a.AuctionItemID == null)
            {
                Create(db, a);
            }
            else
            {
                Update(db, a);
            }
        }

        public static AuctionItem GetAuctionItemByID(ApplicationDbContext db, int id)
        {
            return db.AuctionItems.Where(a => a.AuctionItemID == id).Include("Category").SingleOrDefault();
        }

        public static void Update(ApplicationDbContext db, AuctionItem a)
        {
            if (!a.AuctionItemID.HasValue) { throw new ArgumentException("Auction item has no ID"); }
            AuctionItem old = GetAuctionItemByID(db, a.AuctionItemID.Value) ?? throw new ArgumentException("Auction Item with that ID does not exist in the database.");
            db.Entry(old).State = EntityState.Detached;

            //a.Category = db.Categories.Where(c => c.CategoryID == a.Category.CategoryID).SingleOrDefault();
            db.Entry(a).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void Delete(ApplicationDbContext db, AuctionItem a)
        {
            db.Entry(a).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
}