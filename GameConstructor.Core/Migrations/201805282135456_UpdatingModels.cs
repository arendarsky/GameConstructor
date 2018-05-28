namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "PictureId", "dbo.Pictures");
            DropIndex("dbo.Games", new[] { "PictureId" });
            RenameColumn(table: "dbo.Games", name: "PictureId", newName: "Picture_Id");
            AlterColumn("dbo.Games", "Picture_Id", c => c.Int());
            CreateIndex("dbo.Games", "Picture_Id");
            AddForeignKey("dbo.Games", "Picture_Id", "dbo.Pictures", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Picture_Id", "dbo.Pictures");
            DropIndex("dbo.Games", new[] { "Picture_Id" });
            AlterColumn("dbo.Games", "Picture_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Games", name: "Picture_Id", newName: "PictureId");
            CreateIndex("dbo.Games", "PictureId");
            AddForeignKey("dbo.Games", "PictureId", "dbo.Pictures", "Id", cascadeDelete: true);
        }
    }
}
