using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AuctionSite.Models
{
    public class ItemImage
    {
        public int ItemImageID  { get; set; }
        public string Path { get; set; }

        private ItemImage() {}

        public ItemImage(string path)
        {
            Path = path;
        }

        public static ItemImage DefaultImage()
        {
            return new ItemImage() { ItemImageID = 0, Path = WebConfigurationManager.AppSettings["DefaultImageSrc"] };
        }
    }
}