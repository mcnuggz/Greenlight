﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Greenlight.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int GameID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Game Game { get; set; }
        public virtual Order Order { get; set; }
    }
}