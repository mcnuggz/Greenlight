using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Greenlight.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public virtual ICollection<Game> GameID { get; set; }
        public DateTime CheckoutTime { get; set; }
        public double Total { get; set; }
    }
}