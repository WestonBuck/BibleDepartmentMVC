using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using MVC_Badge_System.Models;
using System.Web;

namespace MVC_Badge_System.Controllers
{
    public class GiftController : Controller
    {

        public ActionResult TreeIndex()
        {
            return View();
        }

		public ActionResult GridIndex()
        {
            User student = LoginController.GetSessionUser();
            int shortList = 0;
            int nextList = 0;
            int longest = 0; // keeps track of the longest list of badges

            GridViewModel GVM = new GridViewModel();
            List<List<BadgeViewModel>> allBadgesList = new List<List<BadgeViewModel>>();
            
            List<Badge> tempCoreList = Db.Db.GetBadges(BadgeType.Apple);

            int i = 0;
            foreach (Badge b in tempCoreList) {
                BadgeViewModel tempBVM = new BadgeViewModel();

                tempBVM.badge = b;
                
                allBadgesList.Add(new List<BadgeViewModel>()); // 1 list per core

                allBadgesList[i].Add(tempBVM);

                List<Badge> tempCompList = Db.Db.GetPrerequisites(b.BadgeId);

                foreach(Badge c in tempCompList) {
                    BadgeViewModel tempCompBadge = new BadgeViewModel();
                    tempCompBadge.badge = c;

                    allBadgesList[i].Add(tempCompBadge);  // add all of a core's prereqs to its list
                } // end foreach c

                i++; // move to the next list
            } // end foreach b

            // keep track of shortest list
            for (int j = 0; j < allBadgesList.Count; j++)
            {
                if (allBadgesList[j].Count < allBadgesList[shortList].Count)
                {
                    nextList = shortList;
                    shortList = j;
                } // end if count
            }


            ////////////////////////////////////
            // The badges the user actually has
            ////////////////////////////////////
            List<Gift> tempGiftList = Db.Db.GetGiftsGivenTo(student.UserId);

            List<Badge> tempGCores = new List<Badge>();
            List<Badge> tempGComps = new List<Badge>();
            List<Badge> tempComendsHold = new List<Badge>();
            List<Badge> tempGComends;

            foreach (Gift g in tempGiftList)
            {
                switch (g.BadgeGift.Type)
                {
                    case BadgeType.Apple:
                        tempGCores.Add(g.BadgeGift);
                        break;
                    case BadgeType.Flower:
                        tempGComps.Add(g.BadgeGift);
                        break;
                    case BadgeType.Leaf:
                        tempComendsHold.Add(g.BadgeGift);
                        break;
                    default:
                        Console.WriteLine("How'd we get this wrong?");
                        break;
                }
            }

            tempGComends = tempComendsHold.GroupBy(p => p.BadgeId).Select(g => g.First()).ToList(); // make sure to only show each comend badge once

            // activate cores and competencies that are obtained 
            foreach (Badge core in tempGCores)
            {
                foreach (List<BadgeViewModel> relList in allBadgesList)
                {
                    if (relList[0].badge.BadgeId == core.BadgeId)
                    {
                        foreach (BadgeViewModel bvm in relList)
                        {
                            bvm.obtained = true; // if you have a core, you have all its competencies
                        } // end foreach bvm
                    } // end if
                } // end foreach relList
            } // end foreach core

            // activate all competencies that aren't associated with an activated core
            foreach (Badge comp in tempGComps)
            {
                foreach (List<BadgeViewModel> relList in allBadgesList)
                {
                    foreach (BadgeViewModel bvm in relList)
                    {
                        if (bvm.badge.BadgeId == comp.BadgeId)
                        {
                            bvm.obtained = true;
                        }
                    } // end foreach bvm
                } // end foreach relList
            } // end foreach comp

            // add all comendation badges to the list list
            foreach (Badge comend in tempGComends)
            {
                BadgeViewModel tempCBVM = new BadgeViewModel();
                tempCBVM.obtained = true;
                tempCBVM.badge = comend;

                allBadgesList[shortList].Add(tempCBVM);

                // keep track of the shortest list
                if (allBadgesList[shortList].Count >= allBadgesList[0].Count)
                {
                    shortList = 0;
                    for (int index = 0; index < allBadgesList.Count; index++)
                    {

                        if (allBadgesList[shortList].Count > allBadgesList[index].Count)
                        {
                            shortList = index;
                        } // end if
                    } // end for
                } // end if
            } // end foreach comend
            // at this point, we have a list of lists w/ counts that are w/i 1 element of each other in length

            for(int j = 0; j < allBadgesList.Count; j++)
            {
                if(allBadgesList[j].Count > allBadgesList[longest].Count)
                {
                    longest = j;
                }
            }

            GVM.student = student;
            GVM.grid = allBadgesList;
            GVM.longest = longest;

            return View(GVM);
        }

        /// <summary>
        /// Get: TestGiftsModal.
        /// Proof of concept for using the GetGifts partial view
        /// </summary>
        /// <returns></returns>
        public ActionResult TestGiftsModal()
        {
            return View();
        }
        
        /// <summary>
        /// GET: GetGifts.
        /// Returns the gifts received by the student for a particular badge. 
        /// The contents should be displayed in a modal popup (see TestGiftsModal)
        /// </summary>
        /// <param name="studentId">User ID for the student receving the badge</param>
        /// <param name="badgeId">Badge ID for the badge received</param>
        /// <returns></returns>
        public ActionResult GetGiftsReceived(int studentId, int badgeId)
        {
            User recip = Db.Db.GetUser(studentId);
            Badge badge = Db.Db.GetBadge(badgeId);
            if (recip == null || recip.UserType != UserType.Student)
            {
                throw new HttpException(404, "Invalid recipient!");
            }
            if (badge == null)
            {
                throw new HttpException(404, "Invalid badge!");
            }
            List<Gift> gifts = Db.Db.GetGiftsGivenTo(recip.UserId, badgeId);

            return PartialView(gifts);
        }
    }
}