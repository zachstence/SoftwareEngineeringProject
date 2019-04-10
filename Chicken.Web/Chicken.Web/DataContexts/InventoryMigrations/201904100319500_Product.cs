namespace Chicken.Web.DataContexts.InventoryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Inventories");
            DropPrimaryKey("dbo.Inventories");
            AddColumn("dbo.Inventories", "ProductID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Inventories", "ProductName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Inventories", "Description", c => c.String(nullable: false));
            AddColumn("dbo.Inventories", "ImagePath", c => c.String());
            AddColumn("dbo.Inventories", "UnitPrice", c => c.Double());
            AddPrimaryKey("dbo.Inventories", "ProductID");
            AddForeignKey("dbo.CartItems", "ProductId", "dbo.Inventories", "ProductID", cascadeDelete: true);
            DropColumn("dbo.Inventories", "Id");
            DropColumn("dbo.Inventories", "Name");
            DropColumn("dbo.Inventories", "Cost");
            DropColumn("dbo.Inventories", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventories", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Inventories", "Cost", c => c.Double(nullable: false));
            AddColumn("dbo.Inventories", "Name", c => c.String());
            AddColumn("dbo.Inventories", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Inventories");
            DropPrimaryKey("dbo.Inventories");
            DropColumn("dbo.Inventories", "UnitPrice");
            DropColumn("dbo.Inventories", "ImagePath");
            DropColumn("dbo.Inventories", "Description");
            DropColumn("dbo.Inventories", "ProductName");
            DropColumn("dbo.Inventories", "ProductID");
            AddPrimaryKey("dbo.Inventories", "Id");
            AddForeignKey("dbo.CartItems", "ProductId", "dbo.Inventories", "Id", cascadeDelete: true);
        }
    }
}
