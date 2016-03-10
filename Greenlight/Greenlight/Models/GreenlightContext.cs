using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Greenlight.Models
{
    public class GreenlightContext : DbContext
    {
        public GreenlightContext() : base("DefaultConnection")
        {

        }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<Greenlight.Models.Product> Products { get; set; }
    }
}