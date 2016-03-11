namespace Greenlight.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Greenlight.Models.GreenlightContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Greenlight.Models.GreenlightContext context)
        {
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Name = "Call of Duty: Lemonade Stand",
                    Genre = "Simulation, Action",
                    Description = "It's hot. People are thirsty, and you are the only lemonade stand around! Manage your profits and satisify all of your customers! Weather affects everything in this game, and no two customers are the same. Are you prepared to sell lemonade?",
                    Price = 24.99M,
                    ImagePath = "../Images/lemonade.jpg",
                    Demo = "../DemoFile/demo1.jpg",
                    FullGame = "../GameFile/full1.jpg"
                    
                }
                
            };
            games.ForEach(g => context.Games.Add(g));
        }
    }
}
