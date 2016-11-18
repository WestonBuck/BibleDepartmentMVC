using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [HttpPost]
        public ActionResult Confirmation(string studentName, string badgeName, string comment)
        {
            ViewBag.studentName = studentName;
            ViewBag.badgeName = "badgeName";
            ViewBag.comment = comment;
            ViewBag.Message = "This is the confirmation page";
            return View();
        }

    }
}