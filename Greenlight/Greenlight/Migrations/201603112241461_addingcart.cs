namespace Greenlight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingcart : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "State", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "ZipCode", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Orders", "Username");
            DropColumn("dbo.Orders", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Email", c => c.String());
            AddColumn("dbo.Orders", "Username", c => c.String());
            AlterColumn("dbo.Orders", "Total", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "ZipCode", c => c.String());
            AlterColumn("dbo.Orders", "State", c => c.String());
            AlterColumn("dbo.Orders", "City", c => c.String());
            AlterColumn("dbo.Orders", "Address", c => c.String());
            AlterColumn("dbo.Orders", "LastName", c => c.String());
            AlterColumn("dbo.Orders", "FirstName", c => c.String());
            AlterColumn("dbo.OrderDetails", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Games", "Price", c => c.String(nullable: false));
        }
    }
}
