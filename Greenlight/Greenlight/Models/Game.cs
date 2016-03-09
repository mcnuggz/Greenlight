using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace Greenlight.Models
{
    public class Game
    {
        public Game()
        {
            Genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
        }

        public int GameID { get; set; }
        [Required]
        public string GameName { get; set; }
        [Required]
        [EnumDataType(typeof(Genre))]
        public ICollection<Genre> Genres { get; set; }
        [ForeignKey("DeveloperID")]
        public Developer Developer { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}