using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AuctionSite.Models
{
    public class Bid
    {
        [Key, Column(Order = 0)]
        public string ApplicationUserID { get; set; }

        [Key, Column(Order = 1)]
        public int AuctionItemID { get; set; }

        public decimal Price { get; set; }
    }
}