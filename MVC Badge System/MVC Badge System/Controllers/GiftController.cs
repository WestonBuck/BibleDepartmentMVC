using System.Web.Mvc;
using System.Collections.Generic;
using MVC_Badge_System.Models;
using System.Web;

namespace MVC_Badge_System.Controllers
{
    public class GiftController : Controller
    {

        public ActionResult TreeIndex()
        {
            return View();
        }

        public ActionResult GridIndex()
        {
            return View();
        }

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
            User recip = Db.Db.GetUser(studentId);
            Badge badge = Db.Db.GetBadge(badgeId);
            if (recip == null || recip.UserType != UserType.Student)
            {
                throw new HttpException(404, "Invalid recipient!");
            }
            if (badge == null)
            {
                throw new HttpException(404, "Invalid badge!");
            }
            List<Gift> gifts = Db.Db.GetGiftsGivenTo(recip.UserId, badgeId);

            return PartialView(gifts);
        }
    }
}