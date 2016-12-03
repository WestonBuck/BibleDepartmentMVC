using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Badge_System.Models
{
    public class Prerequisite
    {
        public Prerequisite()
        {
            PrerequisiteId = null;
            ParentId = null;
            ChildId = null;
        }

        public int? PrerequisiteId { get; set; }
        public int? ParentId { get; set; }
        public int? ChildId { get; set; }

        public Badge Parent { get; set; }
        public Badge Child { get; set; }
    }
}