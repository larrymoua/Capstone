namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updaters : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CoaxialCables", "CableQuantity", c => c.Double(nullable: false));
            AddColumn("dbo.CoaxialCables", "ConnecterQuantity", c => c.Double(nullable: false));
            AddColumn("dbo.CoaxialCables", "HeatShrinkQuantity", c => c.Double(nullable: false));
            AlterColumn("dbo.CoaxialCables", "PartName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CoaxialCables", "PartName", c => c.Int(nullable: false));
            DropColumn("dbo.CoaxialCables", "HeatShrinkQuantity");
            DropColumn("dbo.CoaxialCables", "ConnecterQuantity");
            DropColumn("dbo.CoaxialCables", "CableQuantity");
        }
    }
}
