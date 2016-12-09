using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MVC_Badge_System.Models;

namespace MVC_Badge_System.Controllers
{
    public class HomeController : Controller
    {
        public static Tuple<IEnumerable<Gift>, IEnumerable<DefaultBadge>> GetTreeData(int? userId)
        {
            IEnumerable<Gift> gifts = Db.Db.GetGiftsGivenTo(userId);
            IEnumerable<DefaultBadge> badges = Db.Db.GetAllDefaultBadges();
            Tuple<IEnumerable<Gift>, IEnumerable<DefaultBadge>> data = new Tuple<IEnumerable<Gift>, IEnumerable<DefaultBadge>>(gifts, badges);
            return data;
        }

        public ActionResult Index()
        {
            if (!LoginController.IsSessionValid())
            {
                return RedirectToAction("Login", "Login",
                    new {returnUrl = System.Web.HttpContext.Current.Request.Url.PathAndQuery});
            }

            return View(GetTreeData(LoginController.GetSessionUser().UserId));
        }
        public ActionResult GiveBadge()
        {
            User signedin = new Models.User();
            ViewBag.Message = "Your Give Badge tab page";
            signedin = LoginController.GetSessionUser();
            return View(signedin);
        }


        //pass user recpeient and user sender, get info from those
        public ActionResult Confirmation(string recepientid, string badgeid, string comment)
        {
            Badge newBadge = new Badge();
            //get our recepient
            int recepientID = Int32.Parse(recepientid);
            User Recepient = new Models.User();
            Recepient = Db.Db.GetUser(recepientID);

            //get our sender
            //access the user who is logged in
            User sender = new User();
            sender = LoginController.GetSessionUser();

      

            //create a new gift to add to the DB
            int convertBadgeID = Int32.Parse(badgeid);
            Gift newGift = new Gift();
            DateTime? GiftTime = DateTime.Now;                  //get the current date
            newGift.GiftDate = GiftTime;
            newGift.BadgeId = convertBadgeID;
            newGift.SenderId = sender.UserId;
            newGift.RecipientId = Recepient.UserId;
            newGift.Comment = comment;
            MVC_Badge_System.Db.Db.CreateGift(newGift);         //add gift to db


            //create a confdata model for the view
            ConfirmationData confdata = new ConfirmationData();
            confdata.name = Recepient.FirstName + " " + Recepient.LastName;
            newBadge = Db.Db.GetBadge(convertBadgeID);
            confdata.badge = newBadge.Name;
            confdata.comment = comment;
           // confdata.username = sender. + " " + sender.lastname;

            //Do query to get the email connected to the name
            EmailManager.SendTextEmail(comment, "YOU RECIEVED A BADGE", Recepient.Email, "GSTBADGESYSTEM", "noreply", response => { });
            return View(confdata);
        }


        public ActionResult CreateEditBadge()
        {
            ViewBag.Message = "Your Give Badge tab page";

            return View();
        }
    }
}