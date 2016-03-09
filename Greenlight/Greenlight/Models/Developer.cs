using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greenlight.Models
{
    public class Developer
    {
        public Developer()
        {
            Games = new List<Game>();
        }
        public int DeveloperID { get; set; }
        public string DeveloperName { get; set; }
        public double DeveloperRating { get; set; }
        public ICollection<Game> Games { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}