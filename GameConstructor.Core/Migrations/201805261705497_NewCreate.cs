namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Name", c => c.String());
            AddColumn("dbo.Games", "Source", c => c.String());
            AddColumn("dbo.Games", "Popularity", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "User_Id", c => c.Int());
            AddColumn("dbo.Questions", "Game_Id", c => c.Int());
            CreateIndex("dbo.Characteristics", "GameId");
            CreateIndex("dbo.Games", "User_Id");
            CreateIndex("dbo.Questions", "Game_Id");
            AddForeignKey("dbo.Characteristics", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "Game_Id", "dbo.Games", "Id");
            AddForeignKey("dbo.Games", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Questions", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Characteristics", "GameId", "dbo.Games");
            DropIndex("dbo.Questions", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "User_Id" });
            DropIndex("dbo.Characteristics", new[] { "GameId" });
            DropColumn("dbo.Questions", "Game_Id");
            DropColumn("dbo.Games", "User_Id");
            DropColumn("dbo.Games", "Popularity");
            DropColumn("dbo.Games", "Source");
            DropColumn("dbo.Games", "Name");
        }
    }
}
