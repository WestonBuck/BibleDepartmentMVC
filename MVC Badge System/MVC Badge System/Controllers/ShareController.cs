using System.Web;
using System.Web.Mvc;
using MVC_Badge_System.Models;

namespace MVC_Badge_System.Controllers
{
    public class ShareController : Controller
    {
        // GET: Share
        public ActionResult Index(string id)
        {
            User user = Db.Db.GetUserFromShareableHash(id);
            if (user == null)
            {
                throw new HttpException(404, "Nothing found.");
            }

            return View("~/Views/Home/Index.cshtml", HomeController.GetTreeData(user.UserId));
        }
    }
}