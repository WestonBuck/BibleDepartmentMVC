using Newtonsoft.Json;
using System;
using System.Net;

namespace GoogleAuth_Domain
{
    public class GoogleInterface
    {
        private const string _clientId = "639418916169-ghje86s65jguc9o1jfn3u9f8ibkd74hb.apps.googleusercontent.com";
        private const string _clientSecret = "8Tm6yRwrOupU_cmiQ8p49PGE";
        private const string _baseUrl = "https://www.googleapis.com/oauth2/";

        /// <summary>
        /// Get a Google OAuth 2.0 token for a user
        /// </summary>
        /// <param name="code">One-time access code to login</param>
        /// <param name="redirectUri">Where Google should redirect the user upon authenticating</param>
        /// <returns>Google OAuth 2.0 token</returns>
        public static TokenJson GetToken(string code, string redirectUri)
        {
            TokenJson token;
            if (code == null || code.Length == 0)
            {
                throw new ArgumentNullException("code");
            }
            if (redirectUri == null || redirectUri.Length == 0)
            {
                throw new ArgumentNullException("redirectUri");
            }
            string endpoint = _baseUrl + "v4/token";
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                try
                {
                    var content = client.UploadString(endpoint, "&code=" + code + "&client_id=" + _clientId + "&client_secret=" + _clientSecret + "&redirect_uri=" + redirectUri + "&grant_type=authorization_code");
                    token = JsonConvert.DeserializeObject<TokenJson>(content);
                }
                catch (System.Net.WebException e)
                {
                    //invalid parameters
                    throw new ArgumentException("Unable to connect to Google", e);
                }
                return token;
            }
        }

        /// <summary>
        /// Get information about a Google OAuth 2.0 token
        /// </summary>
        /// <param name="token">Google OAuth 2.0 token</param>
        /// <returns>Google OAuth 2.0 token information</returns>
        public static TokenInfoJson GetTokenInfo(TokenJson token)
        {
            TokenInfoJson tokenInfo;
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }
            if (token.access_token == null || token.access_token.Length == 0)
            {
                throw new ArgumentException("token");
            }
            string endpoint = _baseUrl + "v2/tokeninfo?access_token=" + token.access_token;
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers[HttpRequestHeader.Authorization] = token.GetAuthorizationHeader();
                try
                {
                    var content = client.DownloadString(endpoint);
                    tokenInfo = JsonConvert.DeserializeObject<TokenInfoJson>(content);
                }
                catch (System.Net.WebException e)
                {
                    //invalid token
                    throw new ArgumentException("Unable to connect to Google", e);
                }

            }
            return tokenInfo;
        }

        /// <summary>
        /// Revoke a Google OAuth 2.0 token so it can no longer be used to connect to Google. It's ok if the token is invalid or has already expired.
        /// </summary>
        /// <param name="token">Google OAuth 2.0 token</param>
        public static void RevokeToken(TokenJson token)
        {
            if (token != null && token.access_token != null && token.access_token.Length > 0)
            {
                string endpoint = "https://accounts.google.com/o/oauth2/revoke?token=" + token.access_token;

                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers[HttpRequestHeader.Authorization] = token.GetAuthorizationHeader();
                    try
                    {
                        var content = client.DownloadString(endpoint);
                    }
                    catch (System.Net.WebException e)
                    {
                        //ignore this exception; it means the token already expired or was invalid
                    }

                }
            }
        }

        /// <summary>
        /// Get the Google Login URL for a user
        /// </summary>
        /// <param name="scope">Access needed for the token</param>
        /// <param name="redirectAfterLoginUri">Where Google should redirect after authenticating the user</param>
        /// <param name="state">App-specific state information (cannot pass custom url parameters in the redirectAFterLoginUri)</param>
        /// <param name="homeDomain">Constrain user to a specific home domain (i.e. @example.com)</param>
        /// <returns></returns>
        public static string GetLoginUrl(string scope, string redirectAfterLoginUri, string state = "", string homeDomain = null)
        {
            string homeDomainParam;
            if (scope == null || scope.Length == 0)
            {
                throw new ArgumentNullException("scope");
            }
            if (redirectAfterLoginUri == null || redirectAfterLoginUri.Length == 0)
            {
                throw new ArgumentNullException("redirectAfterLoginUri");
            }
            if (state == null)
            {
                state = "";
            }
            if (homeDomain == null || homeDomain.Length == 0)
            {
                homeDomainParam = "";
            }
            else
            {
                homeDomainParam = "&hd=" + homeDomain;
            }
            string endpoint = "https://accounts.google.com/o/oauth2/v2/auth";
            string loginUrl = endpoint + "?response_type=code&scope=" + scope + "&redirect_uri=" + redirectAfterLoginUri + "&client_id=" + _clientId + "&state=" + state + homeDomainParam;
            return loginUrl;
        }

        /// <summary>
        /// Get the user's basic profile, as determined by Google
        /// </summary>
        /// <param name="token">A valid, unexpired access token provided by Google</param>
        /// <returns>A user's basic Google profile</returns>
        public static UserJson GetUserInfo(TokenJson token)
        {
            UserJson user;
            string endpoint = _baseUrl + "v2/userinfo";

            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers[HttpRequestHeader.Authorization] = token.GetAuthorizationHeader();
                
                try
                {
                    var content = client.DownloadString(endpoint);
                    user = JsonConvert.DeserializeObject<UserJson>(content);
                }
                catch (System.Net.WebException e)
                {
                    throw new ArgumentException("Unable to connect to Google", e);
                }
                
            }
            return user;
        }
    }
}
