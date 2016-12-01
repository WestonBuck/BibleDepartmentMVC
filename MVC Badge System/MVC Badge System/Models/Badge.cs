using System;
using System.Collections.Generic;

namespace MVC_Badge_System.Models
{
    public enum BadgeType
    {
        Apple,
        Flower,
        Leaf
    }
    public class Badge
    {
        public Badge()
        {
            BadgeId = null;
            Description = null;
            Type = null;
            RetirementDate = null;
            BeginDate = null;
            Name = null;
            SelfGive = null;
            StudentGive = null;
            StaffGive = null;
            FacultyGive = null;
        }
        public int? BadgeId { get; set; }
        public string Description { get; set; }
        public BadgeType? Type { get; set; }
        public DateTime? RetirementDate { get; set; }
        public DateTime? BeginDate { get; set; }
        public string Name { get; set; }
        public bool? SelfGive { get; set; }
        public bool? StudentGive { get; set; }
        public bool? StaffGive { get; set; }
        public bool? FacultyGive { get; set; }
        public List<Badge> Prerequisites { get; set; }
    }
}