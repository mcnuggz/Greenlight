using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Greenlight.Models
{
    public class Paypal
    {
        public string CMD { get; set; }
        public string Business { get; set; }
        public string @Rerun { get; set; }
        public string CancelReturn { get; set; }
        public string CurrencyCode { get; set; }
        public string ItemName { get; set; }
        public string Amount { get; set; }
    }
}