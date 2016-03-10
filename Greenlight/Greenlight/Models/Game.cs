using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Greenlight.Models
{
    public class Game
    {
        public Game()
        {
            this.Users = new List<User>();
        }
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required, StringLength(1000), Display(Name ="Game Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name="Cover Art")]
        public string ImagePath { get; set; }  
        [Display(Name="Demo File")] 
        public string Demo { get; set; }
        [Display(Name ="Game File")]
        public string FullGame { get; set; }
        public double? Rating { get; set; }
        [Required, Display(Name ="Price")]
        public string Price { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}