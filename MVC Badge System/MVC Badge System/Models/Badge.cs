using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Badge_System.Models
{
    public class Badge
    {
        public int badge_id { get; set; }
        public string type { get; set; }
        public DateTime retirement_date { get; set; }
        public DateTime start_date { get; set; }
        public string name { get; set; }
        public bool self_give { get; set; }
        public bool student_give { get; set; }
        public bool staff_give { get; set; }
        public bool faculty_give { get; set; }
    }
}