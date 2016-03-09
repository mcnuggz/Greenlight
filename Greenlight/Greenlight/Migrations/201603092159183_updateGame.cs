namespace Greenlight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateGame : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Games", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "Image", c => c.Binary());
        }
    }
}
