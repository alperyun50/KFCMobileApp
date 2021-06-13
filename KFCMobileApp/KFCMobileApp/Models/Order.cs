using System;
using System.Collections.Generic;
using System.Text;

namespace KFCMobileApp.Models
{
    public class Order
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public double order_total { get; set; }
        public DateTime order_placed { get; set; }
        public bool is_order_completed { get; set; }
        public int user_id { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}
