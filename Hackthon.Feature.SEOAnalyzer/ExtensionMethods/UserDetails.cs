using Sitecore.Data;
using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackthon.Feature.SEOAnalyzer.ExtensionMethods
{
    public static class UserDetails
    {
        public const string TokenKey = "UserToken";
        public const string TokenDateKey = "UserTokenDate";
        public static ID CreateUserToken(this User user)
        {
            if (user.IsAuthenticated)
            {
                var token = ID.NewID;
                user.Profile.SetCustomProperty(TokenKey, token.ToString());
                user.Profile.SetCustomProperty(TokenDateKey, DateTime.Now.ToString());
                user.Profile.Save();
                return token;
            }
            else
                return ID.Null;
        }

        public static bool IsTokenValid(this User user, string token, TimeSpan maxAge)
        {
            var tokenId = ID.Null;
            if (ID.TryParse(token, out tokenId))
            {
                var minDate = DateTime.Now.Add(-maxAge);
                var tokenDateString = user.Profile.GetCustomProperty(TokenDateKey);
                var tokenDate = DateTime.MinValue;

                DateTime.TryParse(tokenDateString, out tokenDate);

                if (tokenDate < minDate)
                    return false;

                var storedToken = user.Profile.GetCustomProperty(TokenKey);
                var storedTokenId = ID.NewID;
                if (ID.TryParse(storedToken, out storedTokenId))
                    return storedTokenId == tokenId;
            }

            return false;
        }

    }
}