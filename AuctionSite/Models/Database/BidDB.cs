using System;
using System.Collections.Generic;
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
    }
}