using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Greenlight.Utilities
{
    public static class Common
    {
        public static string FormatJsonString(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return string.Empty;
            }
            if (json.StartsWith("["))
            {
                json = "{\"list\":" + json + "}";
                var formattedText = JObject.Parse(json).ToString(Formatting.Indented);
                formattedText = formattedText.Substring(13, formattedText.Length - 14).Replace("\n ", "\n");
                return formattedText;
            }
            return JObject.Parse(json).ToString(Formatting.Indented);
        }

        public static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }
    }
}