using GoogleAuth_Domain;
using System.Web.Mvc;

namespace MVC_Badge_System.Controllers
{
    /// <summary>
    /// Logs the user into Google.
    /// Common use of the LoginController will be the following:
    /// if (!LoginController.IsSessionValid())
    /// {
    ///     return RedirectToAction("Login", "Login", new { returnUrl = System.Web.HttpContext.Current.Request.Url.PathAndQuery });
    /// }
    /// </summary>
    public class LoginController : Controller
    {

        private const string _scope = "email";
        private const string _homeDomain = "oc.edu";
        
        /// <summary>
        /// GET: Index.
        /// Displays Google User information for the currently logged in user
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (!IsSessionValid())
            {
                return Login();
            }

            UserJson user = GetGoogleUser();
            return View(user);
        }


        /// <summary>
        /// GET: Login.
        /// Sends the user through the Google authentication steps
        /// </summary>
        /// <param name="returnUrl">The URL that the user will be returned to once login is complete. If a return URL is not given, will redirect to /Login/Index.</param>
        /// <returns></returns>
        public ActionResult Login(string returnUrl = null)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Index", null, null, Request.Url.Scheme);
            }
            string redirectTokenUri = Url.Action("Token", null, null, Request.Url.Scheme);
            string redirectLoginUrl = GoogleInterface.GetLoginUrl(_scope, redirectTokenUri, returnUrl, _homeDomain);
            return Redirect(redirectLoginUrl);
        }

        /// <summary>
        /// GET: Logout.
        /// Logs the user out of Google
        /// </summary>
        /// <param name="returnUrl">The URL that the user will be returned to once logout is complete. If a return URL is not given, will return a simple "You have been logged out" page.</param>
        /// <returns></returns>
        public ActionResult Logout(string returnUrl = null)
        {
            GoogleInterface.RevokeToken(GetSessionToken());
            SetSessionToken(null);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View();
        }

        /// <summary>
        /// GET: Token.
        /// Called via the Google API during login; do not call this directly.
        /// </summary>
        /// <param name="code">One-time access code to get authentication token</param>
        /// <param name="state">Current state of the app. Used for URL redirection</param>
        /// <returns></returns>
        public ActionResult Token(string code, string state)
        {
            string redirectUri = Url.Action("Token", null, null, Request.Url.Scheme);
            TokenJson token = GoogleInterface.GetToken(code, redirectUri);
            SetSessionToken(token);
            return Redirect(state);
        }

        /// <summary>
        /// Get the Google user's basic profile
        /// </summary>
        /// <returns>The user's basic profile, as defined by Google</returns>
        public UserJson GetGoogleUser()
        {
            UserJson user;
            if (!IsSessionValid())
            {
                return null;
            }
            user = GoogleInterface.GetUserInfo(GetSessionToken());
            return user;
        }

        /// <summary>
        /// Checks if the current session's token is still valid. If the session is invalid, the user will need to re-login.
        /// </summary>
        /// <returns></returns>
        public static bool IsSessionValid()
        {
            TokenJson token = GetSessionToken();
            if (token != null)
            {
                TokenInfoJson tokenInfo = GoogleInterface.GetTokenInfo(token);
                if (tokenInfo.expires_in > 0)
                {
                    return true;
                }
                //clear out the invalid token
                SetSessionToken(null);
            }
            return false;
        }

        /// <summary>
        /// Get the current session's Google OAuth 2.0 token
        /// </summary>
        /// <returns></returns>
        private static TokenJson GetSessionToken()
        {
            return System.Web.HttpContext.Current.Session["token"] as TokenJson;
        }

        /// <summary>
        /// Set the current session's Google OAuth 2.0 token
        /// </summary>
        /// <param name="token"></param>
        private static void SetSessionToken(TokenJson token)
        {
            System.Web.HttpContext.Current.Session["token"] = token;
        }
    }
}