namespace Chicken.Web.DataContexts.InventoryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ACat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventories", "Category");
        }
    }
}
