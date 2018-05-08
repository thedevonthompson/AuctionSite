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
            return db.AuctionItems.Include("User").Include("Category").ToList();
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
            return db.AuctionItems.Where(a => a.AuctionItemID == id).Include("User").Include("Category").SingleOrDefault();
        }

        public static bool Update(ApplicationDbContext db, AuctionItem a)
        {
            // Check that an item with the given id and corresponding user id exists.
            // This is to prevent a user from editing another user's item.
            if (db.AuctionItems.Where(i => i.AuctionItemID == a.AuctionItemID && i.User.Id == a.User.Id).Count() == 1)
            {
                db.AuctionItems.Attach(a);
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static void Delete(ApplicationDbContext db, AuctionItem a)
        {
            db.Entry(a).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
}