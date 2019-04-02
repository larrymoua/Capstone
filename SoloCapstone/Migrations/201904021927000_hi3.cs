namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hi3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "DueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "DueDate", c => c.DateTime(nullable: false));
        }
    }
}
