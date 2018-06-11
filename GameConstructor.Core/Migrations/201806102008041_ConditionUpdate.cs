namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConditionUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conditions", "ResultId", "dbo.Results");
            DropIndex("dbo.Conditions", new[] { "ResultId" });
            RenameColumn(table: "dbo.Conditions", name: "ResultId", newName: "Result_Id");
            AlterColumn("dbo.Conditions", "Result_Id", c => c.Int());
            CreateIndex("dbo.Conditions", "Result_Id");
            AddForeignKey("dbo.Conditions", "Result_Id", "dbo.Results", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conditions", "Result_Id", "dbo.Results");
            DropIndex("dbo.Conditions", new[] { "Result_Id" });
            AlterColumn("dbo.Conditions", "Result_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Conditions", name: "Result_Id", newName: "ResultId");
            CreateIndex("dbo.Conditions", "ResultId");
            AddForeignKey("dbo.Conditions", "ResultId", "dbo.Results", "Id", cascadeDelete: true);
        }
    }
}
