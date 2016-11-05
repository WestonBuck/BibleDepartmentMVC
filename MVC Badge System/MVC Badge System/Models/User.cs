namespace MVC_Badge_System.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string photo_url { get; set; }
        public string type { get; set; }
        public string shareable_link { get; set; }
    }
}