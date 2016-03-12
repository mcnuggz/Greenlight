namespace Greenlight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "FirstName", c => c.String(nullable: false, maxLength: 160));
            AlterColumn("dbo.Orders", "LastName", c => c.String(nullable: false, maxLength: 160));
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("dbo.Orders", "City", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Orders", "State", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Orders", "ZipCode", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ZipCode", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "State", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "FirstName", c => c.String(nullable: false));
            DropColumn("dbo.Orders", "Email");
        }
    }
}
