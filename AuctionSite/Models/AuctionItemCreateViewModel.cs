using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuctionSite.Models
{
    public class AuctionItemCreateViewModel
    {
        public int? AuctionItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MinPrice { get; set; }

        public int SelectedCategory { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public ICollection<ItemImage> Images { get; set; }

        public AuctionItemCreateViewModel(AuctionItem a) : this()
        {
            AuctionItemID = a.AuctionItemID;
            Name = a.Name;
            Description = a.Description;
            MinPrice = a.MinPrice;

            SelectedCategory = a.Category.CategoryID;
            Images = a.Images;
        }

        public AuctionItemCreateViewModel()
        {
            Categories = new SelectList(CategoryDB.GetAllCategories(new ApplicationDbContext()), "CategoryID", "Name");
        }
    }
}