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
            User user = Db.Db.GetUser(userId);
            if (user == null)
            {
                return null;
            }

            user.ShareableLink = new SoapHexBinary(Guid.NewGuid().ToByteArray()).ToString();
            Db.Db.UpdateUser(user);

            return user.ShareableLink;
        }

        // GET: ShareableLink
        public ActionResult Index(int? id)
        {
            string hash = GenerateShareableHash(id);

            string baseUrl = ShareLinkBaseURL();

            var data = new  { Url = baseUrl + hash };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public static string ShareLinkBaseURL()
        {
            return System.Web.HttpContext.Current.Request.Url?.Scheme + "://" + System.Web.HttpContext.Current.Request.Url?.Authority + System.Web.HttpContext.Current.Request.ApplicationPath?.TrimEnd('/') + "/Share/Index/";
        }
    }
}