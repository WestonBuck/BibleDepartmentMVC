// Keenan Gates and Emily Pielemeier
// MVC Badge System: Sprint 1
// 11/07/16

using MVC_Badge_System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            var range = 3; // number of elements we show in the drop down
            var result = new SearchViewModel();

            result.SearchTerm = filter; // the data in the search bar
            result.SearchResults = new List<User>();
            var allResults = new List<User>();

            // Dummy data to be replaced
            var temp1 = new User(){ user_id = 6, first_name = "bagel", last_name = "tiger", email = "taco.cat@gmail.com", photo_url = "xxx", user_type = "xxx", shareable_link = "xxx" };
            var temp2 = new User() { user_id = 9, first_name = "argle", last_name = "lion", email = "steven@gmail.com", photo_url = "xxx", user_type = "xxx", shareable_link = "xxx" };
            var temp3 = new User() { user_id = 6, first_name = "bass", last_name = "tiger", email = "taco.cat@gmail.com", photo_url = "xxx", user_type = "xxx", shareable_link = "xxx" };
            var temp4 = new User() { user_id = 9, first_name = "angle", last_name = "lion", email = "steven@gmail.com", photo_url = "xxx", user_type = "xxx", shareable_link = "xxx" };
            var temp5 = new User() { user_id = 6, first_name = "baggrt", last_name = "tiger", email = "taco.cat@gmail.com", photo_url = "xxx", user_type = "xxx", shareable_link = "xxx" };
            var temp6 = new User() { user_id = 9, first_name = "ace", last_name = "lion", email = "steven@gmail.com", photo_url = "xxx", user_type = "xxx", shareable_link = "xxx" };
            var temp7 = new User() { user_id = 6, first_name = "bagen", last_name = "tiger", email = "taco.cat@gmail.com", photo_url = "xxx", user_type = "xxx", shareable_link = "xxx" };
            var temp8 = new User() { user_id = 9, first_name = "charlie", last_name = "lion", email = "steven@gmail.com", photo_url = "xxx", user_type = "xxx", shareable_link = "xxx" };
            var temp9 = new User() { user_id = 6, first_name = "balerm", last_name = "tiger", email = "taco.cat@gmail.com", photo_url = "xxx", user_type = "xxx", shareable_link = "xxx" };
            var temp0 = new User() { user_id = 9, first_name = "cadence", last_name = "lion", email = "steven@gmail.com", photo_url = "xxx", user_type = "xxx", shareable_link = "xxx" };
           
            allResults.Add(temp1);
            allResults.Add(temp2);
            allResults.Add(temp3);
            allResults.Add(temp4);
            allResults.Add(temp5);
            allResults.Add(temp6);
            allResults.Add(temp7);
            allResults.Add(temp8);
            allResults.Add(temp9);
            allResults.Add(temp0);

            // add items that start with the data in the search bar
            result.SearchResults = allResults.Where(user => user.first_name.StartsWith(filter)).ToList<User>();
            // sort the items alphabetically
            result.SearchResults = result.SearchResults.OrderBy(user=>user.first_name).ToList<User>();
            // show only the first [insert range here] items of that list
            if (result.SearchResults.Count >= range)
                result.SearchResults = result.SearchResults.GetRange(0, range);

            return View("SearchResult", result);
        }
    }
}