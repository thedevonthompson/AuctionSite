using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionSite.Models
{
    public class ItemImage
    {
        public int ItemImageID  { get; set; }
        public byte[] Data { get; set; }

        private ItemImage() {}

        public ItemImage(byte[] data)
        {
            Data = data;
        }
    }
}