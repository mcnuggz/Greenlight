namespace Greenlight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserNameToOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Username");
        }
    }
}
