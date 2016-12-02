using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Badge_System.Models
{
    public class Gift
    {
        public Gift()
        {
            GiftDate = null;
            GiftId = null;
            BadgeId = null;
            SenderId = null;
            RecipientId = null;
            TreeLocX = null;
            TreeLocY = null;
            Comment = null;
        }
        public DateTime? GiftDate { get; set; }
        public int? GiftId { get; set; }
        public int? BadgeId { get; set; }
        public int? SenderId { get; set; }
        public int? RecipientId { get; set; }
        public int? TreeLocX { get; set; }
        public int? TreeLocY { get; set; }
        public string Comment { get; set; }

        public User Sender { get; set; }
        public User Recipient { get; set; }
        public Badge BadgeGift { get; set; }
    }
}