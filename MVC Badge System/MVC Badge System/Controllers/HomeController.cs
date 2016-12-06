using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Badge_System.Models;

namespace MVC_Badge_System.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!LoginController.IsSessionValid())
            {
                return RedirectToAction("Login", "Login", new { returnUrl = System.Web.HttpContext.Current.Request.Url.PathAndQuery });
            }

            return View();
        }
        public ActionResult GiveBadge()
        {
            ViewBag.Message = "Your Give Badge tab page";

            return View();
        }

     
        public ActionResult Confirmation(User Recepient, string badgeName, string comment, User sender)
        {
            var GiftTime = DateTime.Now.TimeOfDay;
            GiftTime = DateTime.Now.TimeOfDay;
            Gift newGift = new Gift();
            User user = new Models.User();
            newGift.Comment = comment;
            newGift.Recipient = Recepient;
            newGift.Sender = sender;
            //newGift.GiftDate = GiftTime;

            
            //_confData.name = studentName;
            //_confData.badge = badgeName;
            //_confData.comment = comment;
            //_confData.userName = userName;

            
            //Do query to get the email connected to the name
            EmailManager.SendTextEmail(comment, "YOU RECIEVED A BADGE", Recepient.Email , "The code sent you this", "noreply", response => {});
            return View();
        }


        public ActionResult CreateEditBadge()
        {
            ViewBag.Message = "Your Give Badge tab page";

            return View();
        }
    }
}