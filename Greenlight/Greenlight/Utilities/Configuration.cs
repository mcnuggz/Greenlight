using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Greenlight.Utilities
{
    public static class Configuration
    {
        private static readonly string AccessToken;

        static Configuration()
        {
            Config = ConfigManager.Instance.GetProperties();
            AccessToken = new OAuthTokenCredential(Config).GetAccessToken();
        }
        public static APIContext GetAPIContext()
        {
            APIContext apiContext = new APIContext(AccessToken) { Config = Config };
            return apiContext;
        }
    }
}