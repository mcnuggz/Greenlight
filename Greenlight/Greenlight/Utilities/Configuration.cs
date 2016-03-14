using PayPal.Api;
using System.Collections.Generic;

namespace Greenlight.Utilities
{
    public static class Configuration
    {
        private static readonly string AccessToken;
        private static readonly Dictionary<string, string> Config;

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