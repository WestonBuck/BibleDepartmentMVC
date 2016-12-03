using GoogleAuth_Domain;
using System.Web.Mvc;

namespace MVC_Badge_System.Controllers
{
    public class LoginController : Controller
    {

        private const string _scope = "profile";
        private const string _homeDomain = "oc.edu";
        
        /// <summary>
        /// GET: Index.
        /// Displays Google User information for the currently logged in user
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ActionResult loginAction;
            if (!CheckForSession(out loginAction))
            {
                return loginAction;
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
        /// <param name="returnUrl">The URL that the user will be returned to once login is complete. If a return URL is not given, will return a simple "You have been logged out" page.</param>
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
        /// Checks if the user has a session with Google, and if the session is still valid. Example usage would be:
        /// ActionResult loginAction;
        /// if (!CheckForSession(out loginAction))
        /// {
        ///     return loginAction;
        /// }
        /// else
        /// {
        /// //continue as normal
        /// }
        /// </summary>
        /// <param name="actionResult">If the user is not logged in: The next ActionResult a Controller should return in order to login in</param>
        /// <param name="returnUrl">The URL that the user will be returned to once login is complete. If a return URL is not given, will return to the current page's URL</param>
        /// <returns></returns>
        public bool CheckForSession(out ActionResult actionResult, string returnUrl = null)
        {
            if (returnUrl == null)
            {
                returnUrl = Request.Url.PathAndQuery;
            }
           
            if (IsSessionValid())
            {
                actionResult = null;
                return true;
            }
            actionResult = Login(returnUrl);
            return false;
        }

        /// <summary>
        /// Checks if the current session's token is still valid.
        /// </summary>
        /// <returns></returns>
        private bool IsSessionValid()
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