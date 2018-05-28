namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingModels1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "UserId", "dbo.Users");
            DropIndex("dbo.Games", new[] { "UserId" });
            RenameColumn(table: "dbo.Games", name: "UserId", newName: "User_Id");
            AlterColumn("dbo.Games", "User_Id", c => c.Int());
            CreateIndex("dbo.Games", "User_Id");
            AddForeignKey("dbo.Games", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "User_Id", "dbo.Users");
            DropIndex("dbo.Games", new[] { "User_Id" });
            AlterColumn("dbo.Games", "User_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Games", name: "User_Id", newName: "UserId");
            CreateIndex("dbo.Games", "UserId");
            AddForeignKey("dbo.Games", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
