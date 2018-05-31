namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharacteristicUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Characteristics", "Game_Id", "dbo.Games");
            DropIndex("dbo.Characteristics", new[] { "Game_Id" });
            RenameColumn(table: "dbo.Characteristics", name: "Game_Id", newName: "GameId");
            AlterColumn("dbo.Characteristics", "GameId", c => c.Int(nullable: false));
            CreateIndex("dbo.Characteristics", "GameId");
            AddForeignKey("dbo.Characteristics", "GameId", "dbo.Games", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characteristics", "GameId", "dbo.Games");
            DropIndex("dbo.Characteristics", new[] { "GameId" });
            AlterColumn("dbo.Characteristics", "GameId", c => c.Int());
            RenameColumn(table: "dbo.Characteristics", name: "GameId", newName: "Game_Id");
            CreateIndex("dbo.Characteristics", "Game_Id");
            AddForeignKey("dbo.Characteristics", "Game_Id", "dbo.Games", "Id");
        }
    }
}
