namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResultsAndConditions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ResultId = c.Int(nullable: false),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Results", t => t.ResultId, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.ResultId)
                .Index(t => t.Game_Id);
            
            AddColumn("dbo.Games", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conditions", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Conditions", "ResultId", "dbo.Results");
            DropIndex("dbo.Conditions", new[] { "Game_Id" });
            DropIndex("dbo.Conditions", new[] { "ResultId" });
            DropColumn("dbo.Games", "Description");
            DropTable("dbo.Conditions");
        }
    }
}
