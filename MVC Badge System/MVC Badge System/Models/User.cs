using Microsoft.CSharp.RuntimeBinder;

namespace MVC_Badge_System.Models
{
    public enum UserType : int
    {
        Admin,
        Faculty,
        Staff,
        Student
    }
    public class User
    {
        public User()
        {
            UserId = null;
            FirstName = null;
            LastName = null;
            Email = null;
            PhotoUrl = null;
            UserType = null;
            ShareableLink = null;
        }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public UserType? UserType { get; set; }
        public string ShareableLink { get; set; }
    }
}