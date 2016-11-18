using System.Web.Mvc;
using System.Collections.Generic;
using MVC_Badge_System.Models;
using System.Collections;

using System;

namespace MVC_Badge_System.Controllers
{
    public class GiftController : Controller
    {
        /// <summary>
        /// Get: TestGiftsModal.
        /// Proof of concept for using the GetGifts partial view
        /// </summary>
        /// <returns></returns>
        public ActionResult TestGiftsModal()
        {
            return View();
        }
        
        /// <summary>
        /// GET: GetGifts.
        /// Returns the gifts received by the student for a particular badge. 
        /// The contents should be displayed in a modal popup (see TestGiftsModal)
        /// </summary>
        /// <param name="studentId">User ID for the student receving the badge</param>
        /// <param name="badgeId">Badge ID for the badge received</param>
        /// <returns></returns>
        public ActionResult GetGiftsReceived(int studentId, int badgeId)
        {
            //FIXME: validate the student id exists, the student id is for a user whose type is student, and the badge id exists
            List<Gift> gifts = new List<Gift>();
            User recip = new User() { UserId = studentId, FirstName = "Avery", LastName = "Goodstudent" };
            //dummy data to be replaced by database action
            gifts.Add(new Gift() { GiftId = 1, BadgeId = badgeId, Sender = new User() { UserId = 2002, FirstName = "John", LastName = "Doe"}, Recipient = recip, Comment = "good job!"});
            gifts.Add(new Gift() { GiftId = 2, BadgeId = badgeId, Sender = new User() { UserId = 2002, FirstName = "Jane", LastName = "Doe" }, Recipient = recip, Comment = "impressive" });

            return PartialView(gifts);
        }
    }
}