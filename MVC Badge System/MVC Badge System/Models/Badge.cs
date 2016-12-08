using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
            SelfGive = false;
            StudentGive = false;
            StaffGive = false;
            FacultyGive = false;
        }
        [DisplayName("Badge ID")]
        public int? BadgeId { get; set; }
        [DisplayName("Description")]
        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }
        [DisplayName("Badge Type")]
        [Required(ErrorMessage = "Badge Type required")]
        public BadgeType? Type { get; set; }
        [DisplayName("Date Retired")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime? RetirementDate { get; set; }
        
        [DisplayName("Date Started")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime? BeginDate { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
        [DisplayName("Given by Self")]
        public bool SelfGive { get; set; }
        [DisplayName("Given by Student")]
        public bool StudentGive { get; set; }
        [DisplayName("Given by Staff")]
        public bool StaffGive { get; set; }
        [DisplayName("Given by Faculty")]
        public bool FacultyGive { get; set; }
        [DisplayName("Image Link")]
        [DataType(DataType.Url)]
        public string ImageLink { get; set; }
        [DisplayName("Prerequisite Badges")]
        public List<Badge> Prerequisites { get; set; }
    }
}