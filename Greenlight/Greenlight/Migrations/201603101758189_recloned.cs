namespace Greenlight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recloned : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Games", "DemoFile");
            DropColumn("dbo.Games", "GameFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "GameFile", c => c.String());
            AddColumn("dbo.Games", "DemoFile", c => c.String());
        }
    }
}
