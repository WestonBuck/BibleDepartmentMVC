using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Badge_System.Models;
using MVC_Badge_System.Db;
using System.Diagnostics;

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

     
        //pass user recpeient and user sender, get info from those
        public ActionResult Confirmation(string recepient, string badgeid, string comment)
        {
            //get our recepient
            int recepientID = Int32.Parse(recepient);
            User Recepient = new Models.User();
            Recepient = Db.Db.GetUser(recepientID);

            //get our sender
            //access the user who is logged in
            User sender = new User();
            //sender = whoever is logged in

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
            ConfirmationData confData = new ConfirmationData();
            confData.name = Recepient.FirstName + " " + Recepient.LastName;
            confData.badge = badgeid;
            confData.comment = comment;
            confData.userName = sender.FirstName + " " + sender.LastName;

            //Do query to get the email connected to the name
            EmailManager.SendTextEmail(comment, "YOU RECIEVED A BADGE", "colby.dial@eagles.oc.edu" , "GSTBADGESYSTEM", "noreply", response => {});
            return View(confData);
        }


        public ActionResult CreateEditBadge()
        {
            ViewBag.Message = "Your Give Badge tab page";

            return View();
        }
    }
}