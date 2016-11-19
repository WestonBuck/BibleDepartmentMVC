using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Badge_System.Models
{
    public class User
    {
        public int UserId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Photo URL")]
        [DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; }
        [DisplayName("User Type")]
        public string UserType { get; set; }
        [DataType(DataType.Url)]
        public string ShareableLink { get; set; }

        public enum Type
        {
            [Description("Student")]
            student,
            [Description("Staff")]
            staff,
            [Description("Faculty")]
            faculty,
            [Description("Administrator")]
            administrator
        }
    }
}