using System;
using RestSharp;
using RestSharp.Authenticators;

namespace MVC_Badge_System
{
    public class EmailManager
    {
        /// <summary>
        /// The Mailgun API key we use.
        /// </summary>
        public const string ApiKey = "key-005e9acf69a95697bf1017ac96b36c05";
        /// <summary>
        /// Our domain, can be changed once we have a real domain.
        /// </summary>
        public const string Server = "sandboxd325fc0c278644bd899073e44acd36a4.mailgun.org";
        /// <summary>
        /// The API URL to use.
        /// </summary>
        public static readonly string ApiUrl = $"https://api.mailgun.net/v3/{Server}";

        /// <summary>
        /// Send the given HTML email to the given address from the given name. The name will be used to create
        /// the from email as <code>name@Server</code> where <see cref="Server">Server is our domain.</see>
        /// <p/>
        /// This method will send the email asynchronously.
        /// </summary>
        /// <param name="html">The HTML to use to build the email.</param>
        /// <param name="subject">The subject for the email.</param>
        /// <param name="toAddress">The email address to send to. In format: <code>Bog &lt;bog@host.com&gt;</code></param>
        /// <param name="fromName">The name to send the email address from.</param>
        /// <param name="from">The address to send the email from, without the host.</param>
        /// <param name="callback">The callback to run after the response from the email server is received.</param>
        public static void SendEmail(string html, string subject, string toAddress, string fromName, string from, Action<IRestResponse> callback)
        {
            var client = new RestClient(ApiUrl);
            var request = new RestRequest("messages", Method.POST);

            SetupEmail(client, request, fromName, from, toAddress, subject);
            request.AddParameter("html", html);

            client.ExecuteAsync(request, callback);
        }

        /// <summary>
        /// Send the given text email to the given address from the given name. The name will be used to create
        /// the from email as <code>name@Server</code> where <see cref="Server">Server is our domain.</see>
        /// <p/>
        /// This method will send the email asynchronously.
        /// </summary>
        /// <param name="text">The text to use to build the email.</param>
        /// <param name="subject">The subject for the email.</param>
        /// <param name="toAddress">The email address to send to. In format: <code>Bog &lt;bog@host.com&gt;</code></param>
        /// <param name="fromName">The name to send the email address from.</param>
        /// <param name="from">The address to send the email from, without the host.</param>
        /// <param name="callback">The callback to run after the response from the email server is received.</param>
        public static void SendTextEmail(string text, string subject, string toAddress, string fromName, string from, Action<IRestResponse> callback)
        {
            var client = new RestClient(ApiUrl);
            var request = new RestRequest("messages", Method.POST);

            SetupEmail(client, request, fromName, from, toAddress, subject);
            request.AddParameter("text", text);

            client.ExecuteAsync(request, callback);
        }

        private static void SetupEmail(RestClient client, RestRequest request, string fromName, string from, string to, string subject)
        {
            request.AddParameter("from", $"{fromName} <{from}@{Server}>");
            request.AddParameter("to", to);
            request.AddParameter("subject", subject);

            client.Authenticator = new HttpBasicAuthenticator("api", ApiKey);
        }
    }
}