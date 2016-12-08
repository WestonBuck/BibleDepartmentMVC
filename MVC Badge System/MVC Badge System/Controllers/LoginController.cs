using GoogleAuth_Domain;
using MVC_Badge_System.Models;
using System;
using System.Collections.Generic;
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
            SetSession(null);
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
            SetSession(token);
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
                SetSession(null);
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
        /// Set the current session's Google OAuth 2.0 token and the corresponding user in the database
        /// </summary>
        /// <param name="token"></param>
        private static void SetSession(TokenJson token)
        {
            System.Web.HttpContext.Current.Session["token"] = token;
            if (token != null)
            {
                UserJson googleUser = GoogleInterface.GetUserInfo(token);
                if (String.IsNullOrEmpty(googleUser.email))
                {
                    throw new ArgumentNullException("Could not get user's email from Google.");   
                }
                List<User> dbUsers = Db.Db.GetUsersSearch(googleUser.email, null);
                if (dbUsers.Count == 0)
                {
                    throw new ArgumentException("User with email (" + googleUser.email + ") could not be found in the database.");
                }
                User curUser = dbUsers[0];//current user for this session

                //update the user's picture in the database if needed
                if (!String.IsNullOrEmpty(googleUser.picture) && (String.IsNullOrEmpty(curUser.PhotoUrl) || !curUser.PhotoUrl.Equals(googleUser.picture)))
                {
                    curUser.PhotoUrl = googleUser.picture;
                    Db.Db.UpdateUser(curUser);
                }

                System.Web.HttpContext.Current.Session["user"] = curUser;
            }
            else
            {
                System.Web.HttpContext.Current.Session["user"] = null;
            }
        }
        /// <summary>
        /// Get the User from the database that is currently logged in
        /// </summary>
        /// <returns></returns>
        public static User GetSessionUser()
        {
            return System.Web.HttpContext.Current.Session["user"] as User;
        }
    }
}