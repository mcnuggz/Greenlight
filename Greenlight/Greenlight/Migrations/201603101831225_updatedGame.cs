namespace Greenlight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Demo", c => c.String());
            AddColumn("dbo.Games", "FullGame", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "FullGame");
            DropColumn("dbo.Games", "Demo");
        }
    }
}
