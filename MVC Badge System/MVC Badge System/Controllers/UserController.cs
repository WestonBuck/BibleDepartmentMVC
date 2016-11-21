// Keenan Gates and Emily Pielemeier
// MVC Badge System: Sprint 1
// 11/07/16

using MVC_Badge_System.Models;
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
                allResults = db.Query<User>("SELECT first_name FirstName, last_name LastName, email Email, photo_url PhotoUrl, user_type UserType, shareable_link ShareableLink FROM USERS WHERE first_name LIKE '@firstName%' COLLATE SQL_Latin1_General_CP1_CI_AS",
                    new
                    {
                        firstName = filter
                    }).AsList();
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