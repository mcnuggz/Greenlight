namespace Greenlight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reinitialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CartID = c.String(),
                        GameID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.GameID, cascadeDelete: true)
                .Index(t => t.GameID);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Genre = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 1000),
                        ImagePath = c.String(),
                        Demo = c.String(),
                        FullGame = c.String(),
                        Rating = c.Double(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
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
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeveloperRating = c.Double(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        GameID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.GameID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.GameID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "GameID", "dbo.Games");
            DropForeignKey("dbo.Games", "Developer_ID", "dbo.Developers");
            DropForeignKey("dbo.Carts", "GameID", "dbo.Games");
            DropForeignKey("dbo.Users", "Game_ID", "dbo.Games");
            DropIndex("dbo.OrderDetails", new[] { "GameID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Users", new[] { "Game_ID" });
            DropIndex("dbo.Games", new[] { "Developer_ID" });
            DropIndex("dbo.Carts", new[] { "GameID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Developers");
            DropTable("dbo.Users");
            DropTable("dbo.Games");
            DropTable("dbo.Carts");
        }
    }
}
