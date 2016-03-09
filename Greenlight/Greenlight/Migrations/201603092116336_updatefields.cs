namespace Greenlight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Developers", "Name", c => c.String());
            AddColumn("dbo.Games", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Games", "Genre", c => c.String(nullable: false));
            AddColumn("dbo.Games", "Description", c => c.String(nullable: false, maxLength: 1000));
            AddColumn("dbo.Games", "Image", c => c.Binary());
            AddColumn("dbo.Games", "ImagePath", c => c.String());
            AddColumn("dbo.Games", "Price", c => c.String(nullable: false));
            DropColumn("dbo.Developers", "DeveloperName");
            DropColumn("dbo.Games", "GameName");
            DropColumn("dbo.Games", "Genres");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "Genres", c => c.String(nullable: false));
            AddColumn("dbo.Games", "GameName", c => c.String(nullable: false));
            AddColumn("dbo.Developers", "DeveloperName", c => c.String());
            DropColumn("dbo.Games", "Price");
            DropColumn("dbo.Games", "ImagePath");
            DropColumn("dbo.Games", "Image");
            DropColumn("dbo.Games", "Description");
            DropColumn("dbo.Games", "Genre");
            DropColumn("dbo.Games", "Name");
            DropColumn("dbo.Developers", "Name");
        }
    }
}
