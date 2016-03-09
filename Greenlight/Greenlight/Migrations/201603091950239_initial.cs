namespace Greenlight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeveloperName = c.String(),
                        DeveloperRating = c.Double(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GameName = c.String(nullable: false),
                        Genres = c.String(nullable: false),
                        Rating = c.Double(),
                        Developer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Developers", t => t.Developer_ID)
                .Index(t => t.Developer_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Game_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.Game_ID)
                .Index(t => t.Game_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Developer_ID", "dbo.Developers");
            DropForeignKey("dbo.Users", "Game_ID", "dbo.Games");
            DropIndex("dbo.Users", new[] { "Game_ID" });
            DropIndex("dbo.Games", new[] { "Developer_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Games");
            DropTable("dbo.Developers");
        }
    }
}
