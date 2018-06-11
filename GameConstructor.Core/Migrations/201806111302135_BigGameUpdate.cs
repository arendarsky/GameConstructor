namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BigGameUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conditions", "Game_Id", "dbo.Games");
            DropIndex("dbo.Conditions", new[] { "Game_Id" });
            AddColumn("dbo.Games", "Conditions", c => c.String());
            DropColumn("dbo.Conditions", "Game_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conditions", "Game_Id", c => c.Int());
            DropColumn("dbo.Games", "Conditions");
            CreateIndex("dbo.Conditions", "Game_Id");
            AddForeignKey("dbo.Conditions", "Game_Id", "dbo.Games", "Id");
        }
    }
}
