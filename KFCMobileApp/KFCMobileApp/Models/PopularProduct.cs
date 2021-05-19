using System;
using System.Collections.Generic;
using System.Text;

namespace KFCMobileApp.Models
{
    public class PopularProduct
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string image_url { get; set; }

        public string FullImageUrl => AppSettings.apiUrl + image_url;
    }
}
