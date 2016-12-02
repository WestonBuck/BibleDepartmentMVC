using System;
using System.Web.Mvc;

namespace MVC_Badge_System.Controllers
{
    public class EmailTestController : Controller
    {
        // GET: EmailTest
        public ActionResult Index()
        {
            // This actually emails me, this is for demo purposes only. Please don't actually use this.
            EmailManager.SendTextEmail("This is a test email.", "Test Email", "DemonWav <demonwav@gmail.com>", "Test User", "noreply", response => {});

            return View();
        }
    }
}