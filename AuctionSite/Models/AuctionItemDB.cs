using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AuctionSite.Models
{
    public class AuctionItemDB
    {

        private static ApplicationDbContext db;
        static AuctionItemDB()
        {
            db = new ApplicationDbContext();
        }

        public static void Create(AuctionItem a)
        {
            db.AuctionItems.Add(a);
            db.SaveChanges();
        }

        public static List<AuctionItem> GetAllAuctionItems()
        {
            return db.AuctionItems.ToList();
        }

        public static void CreateOrUpdate(AuctionItem a)
        {
            if (a.AuctionItemID == null)
            {
                Create(a);
            }
            else
            {
                Update(a);
            }
        }

        public static AuctionItem GetAuctionItemByID(int id)
        {
            return db.AuctionItems.Where(a => a.AuctionItemID == id).SingleOrDefault();
        }

        public static void Update(AuctionItem a)
        {
            if (!a.AuctionItemID.HasValue) { throw new ArgumentException("Auction item has no ID"); }
            AuctionItem old = GetAuctionItemByID(a.AuctionItemID.Value) ?? throw new ArgumentException("Auction Item with that ID does not exist in the database.");
            db.Entry(old).State = EntityState.Detached;

            db.Entry(a).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void Delete(AuctionItem a)
        {
            db.Entry(a).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
}