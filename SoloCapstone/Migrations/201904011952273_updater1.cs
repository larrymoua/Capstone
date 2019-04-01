namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updater1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CoaxialCables", "CableQuantity", c => c.Int(nullable: false));
            AlterColumn("dbo.CoaxialCables", "ConnecterQuantity", c => c.Int(nullable: false));
            AlterColumn("dbo.CoaxialCables", "HeatShrinkQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CoaxialCables", "HeatShrinkQuantity", c => c.Double(nullable: false));
            AlterColumn("dbo.CoaxialCables", "ConnecterQuantity", c => c.Double(nullable: false));
            AlterColumn("dbo.CoaxialCables", "CableQuantity", c => c.Double(nullable: false));
        }
    }
}
