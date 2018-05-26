namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameModelChanged : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageSource = c.String(),
                        IsBorderRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Games", "DisplayingInGameMode", c => c.Boolean(nullable: false));
            AddColumn("dbo.Games", "Picture_Id", c => c.Int());
            CreateIndex("dbo.Games", "Picture_Id");
            AddForeignKey("dbo.Games", "Picture_Id", "dbo.Pictures", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Picture_Id", "dbo.Pictures");
            DropIndex("dbo.Games", new[] { "Picture_Id" });
            DropColumn("dbo.Games", "Picture_Id");
            DropColumn("dbo.Games", "DisplayingInGameMode");
            DropTable("dbo.Pictures");
        }
    }
}
