using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Badge_System.Models
{
    public class BadgeViewModel
    {
        public Badge badges { get; set; }
        public bool obtained = false;
    }
}