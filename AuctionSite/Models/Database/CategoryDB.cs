using System;
using System.Collections.Generic;
using System.Linq;

namespace AuctionSite.Models.Database
{
    public class CategoryDB
    {
        public static ICollection<Category> GetAllCategories(ApplicationDbContext db)
        {
            return db.Categories.ToList();
        }

        public static Category GetCategoryByID(ApplicationDbContext db, int id)
        {
            return db.Categories.Where(c => c.CategoryID == id).SingleOrDefault();
        }
    }
}