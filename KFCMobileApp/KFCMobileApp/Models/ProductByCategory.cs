using System;
using System.Collections.Generic;
using System.Text;

namespace KFCMobileApp.Models
{
    public class ProductByCategory
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string detail { get; set; }
        public int category_id { get; set; }
        public string image_url { get; set; }
    }
}
