using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Badge_System.Models;

namespace MVC_Badge_System.Controllers
{
    public class BadgeController : Controller
    {
        // GET: Badge
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Create()
        {
            Badge badge = new Badge();
            return View(badge);
        }

        [HttpPost]
        public ActionResult Create(Badge badge)
        {
            Db.Db.CreateBadge(badge);
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            Badge badge = Db.Db.GetBadge(id);
            return View(badge);
        }

        [HttpPost]
        public ActionResult Edit(Badge badge)
        {
            Db.Db.UpdateBadge(badge);
            return RedirectToAction("List");
        }

        public ActionResult Details(int id)
        {
            Badge badge = Db.Db.GetBadge(id);
            return View(badge);
        }

        public ActionResult Delete(int id)
        {
            Badge badge = Db.Db.GetBadge(id);
            if (badge != null)
            {
                Db.Db.DeleteBadge(badge);
            }
            return RedirectToAction("List");
        }
    }
}