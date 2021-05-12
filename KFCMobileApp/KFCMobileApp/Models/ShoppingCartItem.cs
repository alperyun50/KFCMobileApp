using System;
using System.Collections.Generic;
using System.Text;

namespace KFCMobileApp.Models
{
    public class ShoppingCartItem
    {
        public int id { get; set; }
        public double price { get; set; }
        public double total_amount { get; set; }
        public int qty { get; set; }
        public string product_name { get; set; }
    }
}
