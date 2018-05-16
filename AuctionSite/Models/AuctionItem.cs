using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuctionSite.Models
{
    public class AuctionItem
    {
        public int? AuctionItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MinPrice { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ItemImage> Images { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }

        public AuctionItem(AuctionItemCreateViewModel v, Category c, ApplicationUser u)
        : this(v, c, u, null) {}

        public AuctionItem(AuctionItemCreateViewModel v, Category c, ApplicationUser u, ICollection<ItemImage> imgs)
        {
            AuctionItemID = v.AuctionItemID;
            Name = v.Name;
            Description = v.Description;
            MinPrice = v.MinPrice;

            User = u;
            Category = c;
            Images = imgs;
        }

        private AuctionItem() { }
    }
}