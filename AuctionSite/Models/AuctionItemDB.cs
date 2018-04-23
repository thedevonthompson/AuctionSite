using System;
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

        public static AuctionItem GetAuctionItemByID(int id)
        {
            return db.AuctionItems.Where(a => a.AuctionItemID == id).SingleOrDefault();
        }

        public static void Update(AuctionItem a)
        {
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