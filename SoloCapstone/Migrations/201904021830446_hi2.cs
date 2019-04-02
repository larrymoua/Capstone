namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hi2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "OrderStatus", c => c.String());
        }
    }
}
