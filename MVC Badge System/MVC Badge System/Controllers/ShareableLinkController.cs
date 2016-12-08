using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.Mvc;
using MVC_Badge_System.Models;

namespace MVC_Badge_System.Controllers
{
    public class ShareableLinkController : Controller
    {
        public static string GenerateShareableHash(int? userId)
        {
            User user = Db.Db.GetUser(LoginController.GetSessionUser().UserId);
            if (user == null)
            {
                return null;
            }

            user.ShareableLink = new SoapHexBinary(Guid.NewGuid().ToByteArray()).ToString();
            Db.Db.UpdateUser(user);

            return user.ShareableLink;
        }

        // GET: ShareableLink
        public ActionResult Index()
        {
            string hash = GenerateShareableHash(LoginController.GetSessionUser().UserId);

            string baseUrl = Request.Url?.Scheme + "://" + Request.Url?.Authority + Request.ApplicationPath?.TrimEnd('/') + "/Share/Index/";

            var data = new  { Url = baseUrl + hash };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}