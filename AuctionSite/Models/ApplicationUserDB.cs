using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionSite.Models
{
    public class ApplicationUserDB
    {
        public static ApplicationUser GetUserByID(ApplicationDbContext db, string id)
        {
            return db.Users.Where(u => u.Id == id).SingleOrDefault();
        }
    }
}