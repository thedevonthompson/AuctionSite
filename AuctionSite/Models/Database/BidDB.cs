using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace AuctionSite.Models.Database
{
    public class BidDB
    {

        public static void Create(ApplicationDbContext db, Bid b)
        {
            db.Bids.Add(b);
            db.SaveChanges();
        }

        public static void CreateOrUpdate(ApplicationDbContext db, Bid b)
        {
            try
            {
                Create(db, b);
            }
            catch (DbUpdateException)
            {

                Update(db, b);
            }
        }

        public static Bid GetBidByID(ApplicationDbContext db, string userID, int itemID)
        {
            return db.Bids.Where(b => b.ApplicationUserID == userID && b.AuctionItemID == itemID).SingleOrDefault();
        }

        public static List<Bid> GetBidsByUserID(ApplicationDbContext db, string userID)
        {
            return db.Bids.Where(b => b.ApplicationUserID == userID).ToList();
        }

        public static void Update(ApplicationDbContext db, Bid b)
        {
            db.Bids.Attach(b);
            db.Entry(b).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}