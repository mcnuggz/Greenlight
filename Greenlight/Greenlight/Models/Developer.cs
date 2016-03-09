using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Greenlight.Models
{
    public class Developer
    {
        public Developer()
        {
            Games = new List<Game>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public double? DeveloperRating { get; set; }
        
        public virtual ICollection<Game> Games { get; set; }
        
    }
}