namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updater : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ItemName = c.String(nullable: false, maxLength: 128),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ItemName);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Images");
        }
    }
}
