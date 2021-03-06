using System;
using System.Collections.Generic;
using System.Text;

namespace KFCMobileApp.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string detail { get; set; }
        public string image_url { get; set; }
        public double price { get; set; }
        public bool is_popular_product { get; set; }
        public int category_id { get; set; }
        public object image_array { get; set; }

        public string FullImageUrl => AppSettings.apiUrl + image_url;
    }
}
