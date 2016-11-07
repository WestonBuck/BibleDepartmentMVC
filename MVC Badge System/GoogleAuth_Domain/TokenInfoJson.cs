namespace GoogleAuth_Domain
{
    public class TokenInfoJson
    {
        public string issued_to { get; set; }
        public string user_id { get; set; }
        public int expires_in { get; set; }
        public string access_type { get; set; }
        public string audience { get; set; }
        public string scope { get; set; }
    }
}
