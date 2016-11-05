namespace MVC_Badge_System.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string UserType { get; set; }
        public string ShareableLink { get; set; }
    }
}