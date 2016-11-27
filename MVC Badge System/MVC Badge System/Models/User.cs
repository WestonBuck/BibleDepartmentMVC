using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Badge_System.Models
{
    public class User
    {
        [DisplayName("ID")]
        public int UserId { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name required")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name required")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }
        [DisplayName("Photo URL")]
        [DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; }
        [DisplayName("Type")]
        [Required(ErrorMessage = "User Type required")]
        public string UserType { get; set; }
        [DisplayName("Shareable Link")]
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