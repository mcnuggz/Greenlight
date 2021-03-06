﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Greenlight.Models
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public string CartID { get; set; }
        public int GameID { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Game Game { get; set; }
    }
}