using System;
using System.Collections.Generic;
using System.Text;

namespace KFCMobileApp.Models
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public int expires_in { get; set; }
        public int creation_time { get; set; }
        public int expiration_time { get; set; }
    }
}
