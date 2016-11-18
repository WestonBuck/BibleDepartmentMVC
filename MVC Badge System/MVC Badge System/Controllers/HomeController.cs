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
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
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
            User _user = new Models.User();
            
            
            _confData.name = studentName;
            _confData.badge = badgeName;
            _confData.comment = comment;
            _confData.userName = userName;

            
            //Do query to get the email connected to the name
            EmailManager.SendTextEmail(comment, "YOU RECIEVED A BADGE", "colby.dial@eagles.oc.edu" , "The code sent you this", "noreply",
                response =>
                {
                    Console.WriteLine("Email successfully sent.");
                });
            return View(_confData);
        }

    }
}