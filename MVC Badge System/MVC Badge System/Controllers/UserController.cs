// Keenan Gates and Emily Pielemeier
// MVC Badge System: Sprint 1
// 11/07/16

using MVC_Badge_System.Models;
using MVC_Badge_System.Db;
using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Collections.Generic;
using Dapper;

namespace MVC_Badge_System.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            List<User> users = Db.Db.GetAllUsers();
            return View(users);

        }

        public ActionResult Create()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            user.ShareableLink = "https://fake.com";//FIXME: generate shareable link later that directs to the tree view
            Db.Db.CreateUser(user);
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            User user = Db.Db.GetUser(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            Db.Db.UpdateUser(user);
            return RedirectToAction("List");
        }

        public ActionResult Details(User user)
        {
            Db.Db.GetUser(user.UserId);
            return View(user);
        }
        
        public ActionResult Delete(int id)
        {
            User user = Db.Db.GetUser(id);
            if (user != null)
            {
                Db.Db.DeleteUser(user);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult GetUser(string filter)
        {
            var range = 5; // number of elements we show in the drop down
            var result = new SearchViewModel();

            result.SearchTerm = filter; // the data in the search bar
            result.SearchResults = new List<User>();
            List<User> allResults;

            // collect the results that match the filter from the data base
            using (IDbConnection db = new SqlConnection(Db.Db.Connection))
            {                                                                                                                                                                                                                               // Case insensitive
                allResults = db.Query<User>("Select FIRST_NAME AS FirstName, LAST_NAME AS LastName, Email, PHOTO_URL AS PhotoUrl, USER_TYPE AS UserType, SHAREABLE_LINK AS ShareableLink From USERS WHERE FIRST_NAME LIKE '" + filter + "%' COLLATE SQL_Latin1_General_CP1_CI_AS").ToList();
            }
            
            // sort the items alphabetically
            result.SearchResults = allResults.OrderBy(user=>user.FirstName).ToList<User>();
            // show only the first [insert range here] items of that list
            if (result.SearchResults.Count >= range)
                result.SearchResults = result.SearchResults.GetRange(0, range);

            return View("SearchResult", result);
        }
    }
}