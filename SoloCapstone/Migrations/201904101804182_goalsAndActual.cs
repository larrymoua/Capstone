namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goalsAndActual : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actuals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monday = c.Int(nullable: false),
                        Tuesday = c.Int(nullable: false),
                        Wednesday = c.Int(nullable: false),
                        Thursday = c.Int(nullable: false),
                        Friday = c.Int(nullable: false),
                        Saturday = c.Int(nullable: false),
                        Sunday = c.Int(nullable: false),
                        GoalFK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goals", t => t.GoalFK, cascadeDelete: true)
                .Index(t => t.GoalFK);
            
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monday = c.Int(nullable: false),
                        Tuesday = c.Int(nullable: false),
                        Wednesday = c.Int(nullable: false),
                        Thursday = c.Int(nullable: false),
                        Friday = c.Int(nullable: false),
                        Saturday = c.Int(nullable: false),
                        Sunday = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actuals", "GoalFK", "dbo.Goals");
            DropIndex("dbo.Actuals", new[] { "GoalFK" });
            DropTable("dbo.Goals");
            DropTable("dbo.Actuals");
        }
    }
}
