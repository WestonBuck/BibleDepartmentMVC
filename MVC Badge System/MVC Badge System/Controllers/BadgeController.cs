using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Badge_System.Controllers
{
    public class BadgeController : Controller
    {
        // GET: Badge
        public ActionResult List()
        {
            return View();
        }
    }
}