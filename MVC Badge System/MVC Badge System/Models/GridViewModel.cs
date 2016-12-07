using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Badge_System.Models
{
    public class GridViewModel
    {
        public List<List<BadgeViewModel>> grid { get; set; }
        public User student; // all users shown in this view will be students
    }
}