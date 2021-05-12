using System;
using System.Collections.Generic;
using System.Text;

namespace KFCMobileApp.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public double price { get; set; }
        public int gty { get; set; }
        public double total_amount { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public Product product { get; set; }
    }
}
