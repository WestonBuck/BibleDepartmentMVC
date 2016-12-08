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
            Badge before = Db.Db.GetBadge(badge.BadgeId);
            if (before == null)
            {
                throw new HttpException(404, "Invalid badge!");
            }
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
        [HttpPost]
        public ActionResult SetPrerequisites(int badgeId, int?[] prerequisiteIds)
        {
            Badge before = Db.Db.GetBadge(badgeId);
            if (before == null)
            {
                throw new HttpException(404, "Invalid badge!");
            }
            if (before.Prerequisites == null)
            {
                before.Prerequisites = new List<Badge>();
            }
            List<int?> beforeIDs = before.Prerequisites.Select(badge => badge.BadgeId).ToList();
            List<int?> afterIDs = new List<int?>(prerequisiteIds);
            //Set comparison [B - A] gives us the new prerequisites
            List<int?> toCreateChildIDs = afterIDs.Except(beforeIDs).ToList();
            //Set comparison [A - B] gives us the prerequisites to delete
            List<int?> toDeleteChildIDs = beforeIDs.Except(afterIDs).ToList(); 
            
            foreach (int childId in toCreateChildIDs)
            {
                Db.Db.CreatePrerequisite(badgeId, childId);
            }
            foreach (int childId in toDeleteChildIDs)
            {
                Db.Db.DeletePrerequisite(badgeId, childId);
            }
            return Json(new { success = true });
        }

        public static List<Badge> GetAllBadges()
        {
            List<Badge> badges = Db.Db.GetAllBadges();
            return badges;
        }
    }
}