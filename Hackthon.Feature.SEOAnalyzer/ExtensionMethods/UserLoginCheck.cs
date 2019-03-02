using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;
using Sitecore.StringExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackthon.Feature.SEOAnalyzer.ExtensionMethods
{
    public class UserLoginCheck : HttpRequestProcessor
    {
        public TimeSpan MaximumAge
        {
            get;
            set;
        }

        public override void Process(HttpRequestArgs args)
        {
            var userValue = args.Context.Request.QueryString["user"];
            var tokenValue = args.Context.Request.QueryString["token"];

            if (!string.IsNullOrEmpty(userValue) && !string.IsNullOrEmpty(tokenValue))
            {
                // find user
                var user = User.FromName(userValue, AccountType.User);
                if (user != null)
                {
                    string userName = userValue;
                    AuthenticationManager.Login(userName);
                    string ticket = Sitecore.Web.Authentication.TicketManager.CreateTicket(userName, @"/sitecore/shell");
                    HttpContext current = HttpContext.Current;
                    if (current != null)
                    {
                        HttpCookie cookie = new HttpCookie(Sitecore.Web.Authentication.TicketManager.CookieName, ticket)
                        {
                            HttpOnly = true
                        };
                        current.Response.AppendCookie(cookie);
                    }
                    // Check token is valid
                    //   if ((user as User).IsTokenValid(tokenValue, MaximumAge))
                    //   {

                    // log user in
                   AuthenticationManager.Login(user as User);
                    //}
                    //else
                    //    Log.Audit("User token has expired for user: '{0}'".FormatWith(user.Name), this);
                }
                else
                    Log.Audit("Failed to locate auto login user " + userValue, this);
            }
        }
    }
}