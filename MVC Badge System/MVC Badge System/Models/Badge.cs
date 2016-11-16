using System;
using System.Collections.Generic;

namespace MVC_Badge_System.Models
{
    public class Badge
    {
        public int BadgeId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime RetirementDate { get; set; }
        public DateTime BeginDate { get; set; }
        public string Name { get; set; }
        public bool SelfGive { get; set; }
        public bool StudentGive { get; set; }
        public bool StaffGive { get; set; }
        public bool FacultyGive { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
		public List<Badge> Prerequisites { get; set; }
    }
}