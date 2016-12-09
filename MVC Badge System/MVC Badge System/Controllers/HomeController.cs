using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Mvc;
using MVC_Badge_System.Models;

namespace MVC_Badge_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly Random random = new Random();

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

        public ActionResult Home()
        {
            if (!LoginController.IsSessionValid())
            {
                return RedirectToAction("Login", "Login",
                    new { returnUrl = System.Web.HttpContext.Current.Request.Url.PathAndQuery });
            }
            User curUser = LoginController.GetSessionUser();
            //go to the tree view if it's a student
            if (curUser.UserType == UserType.Student)
            {
                return RedirectToAction("Index");
            }
            return View(curUser);
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

            try
            {
                Point location = GetNewBadgeLocation(Recepient);
                newGift.TreeLocX = location.X;
                newGift.TreeLocY = location.Y;
            }
            catch (Exception e)
            {
                // ignore for now
            }

            Db.Db.CreateGift(newGift);         //add gift to db

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

        /// <summary>
        /// The computer science gods are looking at this method and crying.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Point GetNewBadgeLocation(User user)
        {
            var treeData = GetTreeData(user.UserId);

            // Bounding triangle
            int triLeftX = 182;
            int triLeftY = 200;
            Point triLeftPoint = new Point(triLeftX, triLeftY);

            int triRightX = 848;
            int triRightY = 200;
            Point triRightPoint = new Point(triRightX, triRightY);

            int triTopX = 515;
            int triTopY = 600;
            Point triTopPoint = new Point(triTopX, triTopY);

            // Excluding box
            int excBottomLeftX = 450;
            int excBottomLeftY = 195;
            Point excBottomLeftPoint = new Point(excBottomLeftX, excBottomLeftY);

            int excTopRightX = 585;
            int excTopRightY = 285;
            Point excTopRightPoint = new Point(excTopRightX, excTopRightY);

            double acceptableDistance = 20.0;

            int counter = 0;

            Point? p = null;
            while (true)
            {
                if (++counter > 10000)
                {
                    break;
                }

                int x = random.Next(182, 849);
                int y = random.Next(200, 601);
                p = new Point(x, y);

                if (!IsInTriangle(p.Value, triLeftPoint, triRightPoint, triTopPoint))
                {
                    continue;
                }

                if (IsInBox(p.Value, excBottomLeftPoint, excTopRightPoint))
                {
                    continue;
                }

                bool good = true;
                foreach (Gift gift in treeData.Item1)
                {
                    if (gift.TreeLocX == null || gift.TreeLocY == null || gift.TreeLocX == 0 || gift.TreeLocY == 0)
                    {
                        continue;
                    }

                    Point giftPoint = new Point(gift.TreeLocX.Value, gift.TreeLocY.Value);
                    double distance = Distance(p.Value, giftPoint);

                    if (distance < acceptableDistance)
                    {
                        good = false;
                        break;
                    }
                }

                if (good)
                {
                    break;
                }

                foreach (DefaultBadge badge in treeData.Item2)
                {
                    if (badge.TreeLocX == null || badge.TreeLocY == null || badge.TreeLocX == 0 || badge.TreeLocY == 0)
                    {
                        continue;
                    }

                    Point badgePoint = new Point(badge.TreeLocX.Value, badge.TreeLocY.Value);
                    double distance = Distance(p.Value, badgePoint);

                    if (distance < acceptableDistance)
                    {
                        good = false;
                        break;
                    }
                }

                if (good)
                {
                    break;
                }
            }

            if (p == null)
            {
                throw new Exception("No suitable place on the tree was found.");
            }

            return p.Value;
        }

        /// <summary>
        /// lol wat http://jsfiddle.net/PerroAZUL/zdaY8/1/
        /// </summary>
        /// <param name="p">The point to check</param>
        /// <param name="p0">One of the points on the triangle</param>
        /// <param name="p1">One of the points on the triangle</param>
        /// <param name="p2">One of the points on the triangle</param>
        /// <returns>True if the point is in the triangle</returns>
        public bool IsInTriangle(Point p, Point p0, Point p1, Point p2)
        {
            double a = (1.0 / 2.0) * (-p1.Y * p2.X + p0.Y * (-p1.X + p2.X) + p0.X * (p1.Y - p2.Y) + p1.X * p2.Y);
            double sign = a < 0 ? -1 : 1;
            double s = (p0.Y * p2.X - p0.X * p2.Y + (p2.Y - p0.Y) * p.X + (p0.X - p2.X) * p.Y) * sign;
            double t = (p0.X * p1.Y - p0.Y * p1.X + (p0.Y - p1.Y) * p.X + (p1.X - p0.X) * p.Y) * sign;

            return s > 0 && t > 0 && (s + t) < 2 * a * sign;
        }

        /// <summary>
        /// Checks if the point is in the box.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public bool IsInBox(Point p, Point p0, Point p1)
        {
            return p0.X < p.X && p.X < p1.X && p0.Y < p.Y && p.Y < p1.Y;
        }

        /// <summary>
        /// Get the distance between two points.
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public double Distance(Point p0, Point p1)
        {
            return Math.Sqrt(Math.Pow(p0.X + p1.X, 2) + Math.Pow(p0.Y + p1.Y, 2));
        }

        public ActionResult CreateEditBadge()
        {
            ViewBag.Message = "Your Give Badge tab page";

            return View();
        }
    }
}