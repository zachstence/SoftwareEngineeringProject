namespace Chicken.Web.DataContexts.BookMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Chick_Wing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Wing", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Wing");
        }
    }
}
