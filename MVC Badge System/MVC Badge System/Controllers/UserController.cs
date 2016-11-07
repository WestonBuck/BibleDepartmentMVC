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
            var temp1 = new User(){ UserId = 6, FirstName = "bagel", LastName = "tiger", Email = "taco.cat@gmail.com", PhotoUrl = "xxx", UserType = "xxx", ShareableLink = "xxx" };
            var temp2 = new User() { UserId = 9, FirstName = "argle", LastName = "lion", Email = "steven@gmail.com", PhotoUrl = "xxx", UserType = "xxx", ShareableLink = "xxx" };
            var temp3 = new User() { UserId = 6, FirstName = "bass", LastName = "tiger", Email = "taco.cat@gmail.com", PhotoUrl = "xxx", UserType = "xxx", ShareableLink = "xxx" };
            var temp4 = new User() { UserId = 9, FirstName = "angle", LastName = "lion", Email = "steven@gmail.com", PhotoUrl = "xxx", UserType = "xxx", ShareableLink = "xxx" };
            var temp5 = new User() { UserId = 6, FirstName = "baggrt", LastName = "tiger", Email = "taco.cat@gmail.com", PhotoUrl = "xxx", UserType = "xxx", ShareableLink = "xxx" };
            var temp6 = new User() { UserId = 9, FirstName = "ace", LastName = "lion", Email = "steven@gmail.com", PhotoUrl = "xxx", UserType = "xxx", ShareableLink = "xxx" };
            var temp7 = new User() { UserId = 6, FirstName = "bagen", LastName = "tiger", Email = "taco.cat@gmail.com", PhotoUrl = "xxx", UserType = "xxx", ShareableLink = "xxx" };
            var temp8 = new User() { UserId = 9, FirstName = "charlie", LastName = "lion", Email = "steven@gmail.com", PhotoUrl = "xxx", UserType = "xxx", ShareableLink = "xxx" };
            var temp9 = new User() { UserId = 6, FirstName = "balerm", LastName = "tiger", Email = "taco.cat@gmail.com", PhotoUrl = "xxx", UserType = "xxx", ShareableLink = "xxx" };
            var temp0 = new User() { UserId = 9, FirstName = "cadence", LastName = "lion", Email = "steven@gmail.com", PhotoUrl = "xxx", UserType = "xxx", ShareableLink = "xxx" };
           
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
            result.SearchResults = allResults.Where(user => user.FirstName.StartsWith(filter)).ToList<User>();
            // sort the items alphabetically
            result.SearchResults = result.SearchResults.OrderBy(user=>user.FirstName).ToList<User>();
            // show only the first [insert range here] items of that list
            if (result.SearchResults.Count >= range)
                result.SearchResults = result.SearchResults.GetRange(0, range);

            return View("SearchResult", result);
        }
    }
}