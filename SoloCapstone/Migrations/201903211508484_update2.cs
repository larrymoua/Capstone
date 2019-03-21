namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchasingManagers",
                c => new
                    {
                        PurchasingManagerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PurchasingManagerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchasingManagers", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.PurchasingManagers", new[] { "ApplicationUserId" });
            DropTable("dbo.PurchasingManagers");
        }
    }
}
