namespace MVC_Badge_System.Models
{
    public class DefaultBadge
    {
        public DefaultBadge()
        {
            BadgeId = null;
            TreeLocX = null;
            TreeLocY = null;
        }

        public int? BadgeId { get; set; }
        public BadgeType? Type { get; set; }
        public int? TreeLocX { get; set; }
        public int? TreeLocY { get; set; }
    }
}