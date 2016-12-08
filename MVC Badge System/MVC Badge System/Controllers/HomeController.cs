using System;
using System.Collections.Generic;
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

            IEnumerable<Gift> gifts = Db.Db.GetGiftsGivenTo(LoginController.GetSessionUser().UserId);
            IEnumerable<DefaultBadge> badges = Db.Db.GetAllDefaultBadges();
            Tuple<IEnumerable<Gift>, IEnumerable<DefaultBadge>> data = new Tuple<IEnumerable<Gift>, IEnumerable<DefaultBadge>>(gifts, badges);

            return View(data);
        }
        public ActionResult GiveBadge()
        {
            ViewBag.Message = "Your Give Badge tab page";

            return View();
        }

     
        public ActionResult Confirmation(string studentName, string badgeName, string comment, string userName)
        {
            ConfirmationData _confData = new ConfirmationData();
            Gift _gift = new Gift();
            User _user = new User();
            
            
            _confData.name = studentName;
            _confData.badge = badgeName;
            _confData.comment = comment;
            _confData.userName = userName;

            
            //Do query to get the email connected to the name
            EmailManager.SendTextEmail(comment, "YOU RECIEVED A BADGE", "colby.dial@eagles.oc.edu" , "The code sent you this", "noreply", response => {});
            return View(_confData);
        }


        public ActionResult CreateEditBadge()
        {
            ViewBag.Message = "Your Give Badge tab page";

            return View();
        }
    }
}