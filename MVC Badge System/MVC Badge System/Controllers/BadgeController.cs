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
            List<Badge> badges = Db.Db.GetAllBadges();
            return View(badges);
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

        public ActionResult SetRetirementDate(int id, string returnActionName = "List", string returnControllerName = "Badge")
        {
            Badge badge = Db.Db.GetBadge(id);
            if (badge == null)
            {
                throw new HttpException(404, "Invalid badge!");
            }
            badge.RetirementDate = DateTime.Now;
            Db.Db.UpdateBadge(badge);
            return RedirectToAction(returnActionName, returnControllerName);
        }

        public ActionResult SetBeginDate(int id, string returnActionName = "List", string returnControllerName = "Badge")
        {
            Badge badge = Db.Db.GetBadge(id);
            if (badge == null)
            {
                throw new HttpException(404, "Invalid badge!");
            }
            badge.BeginDate = DateTime.Now;
            Db.Db.UpdateBadge(badge);
            return RedirectToAction(returnActionName, returnControllerName);
        }
    }
}