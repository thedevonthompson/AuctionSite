using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionSite.Models
{
    public class AuctionItemBidViewModel
    {
        public int? AuctionItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MinPrice { get; set; }
        public DateTime EndDateTime { get; set; }

        public ApplicationUser User { get; set; }
        public Category Category { get; set; }
        public ICollection<ItemImage> Images { get; set; }
        public Bid Bid { get; set; }

        private AuctionItemBidViewModel() { }

        public AuctionItemBidViewModel(AuctionItem a, Bid b)
        {
            AuctionItemID = a.AuctionItemID;
            Name = a.Name;
            Description = a.Description;
            MinPrice = a.MinPrice;
            EndDateTime = a.EndDateTime;
            User = a.User;
            Category = a.Category;
            Images = a.Images;

            Bid = b;
        }

        public static Bid GetHighestBid(ICollection<Bid> bids)
        {
            return bids.Aggregate(null, (Bid maxBid, Bid bid) => 
            {
                if (maxBid == null)
                {
                    return bid;
                }
                else if (bid.Price > maxBid.Price)
                {
                    return bid;
                }
                else
                {
                    return maxBid;
                }
            });
        }

        public static Bid GetBidByUserID(ICollection<Bid> bids, string userID)
        {
            return bids.Where(b => b.ApplicationUserID == userID).SingleOrDefault();
        }
    }
}