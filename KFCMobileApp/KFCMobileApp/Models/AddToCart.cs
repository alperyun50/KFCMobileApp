using System;
using System.Collections.Generic;
using System.Text;

namespace KFCMobileApp.Models
{
    public class AddToCart
    {
        public string price { get; set; }
        public string qty { get; set; }
        public string total_amount { get; set; }
        public int product_id { get; set; }
        public int customer_id { get; set; }
    }
}
