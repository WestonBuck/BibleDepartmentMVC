using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Badge_System.Models
{
    public class Gift
    {
        public int gift_id { get; set; }
        public int badge_id { get; set; }
        public int sender_id { get; set; }
        public int recipient_id { get; set; }
        public int tree_loc_x { get; set; }
        public int tree_loc_y { get; set; }
        public string comment { get; set; }
    }
}