namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects");
            DropForeignKey("dbo.Questions", "Game_Id", "dbo.Games");
            DropIndex("dbo.Effects", new[] { "Answer_Id" });
            DropIndex("dbo.Influences", new[] { "Effect_Id" });
            DropIndex("dbo.Questions", new[] { "Game_Id" });
            RenameColumn(table: "dbo.Effects", name: "Answer_Id", newName: "AnswerId");
            RenameColumn(table: "dbo.Influences", name: "Effect_Id", newName: "EffectId");
            RenameColumn(table: "dbo.Questions", name: "Game_Id", newName: "GameId");
            AddColumn("dbo.Users", "QuestionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Effects", "AnswerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Influences", "EffectId", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "GameId", c => c.Int(nullable: false));
            CreateIndex("dbo.Effects", "AnswerId");
            CreateIndex("dbo.Influences", "EffectId");
            CreateIndex("dbo.Questions", "GameId");
            AddForeignKey("dbo.Effects", "AnswerId", "dbo.Answers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Influences", "EffectId", "dbo.Effects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "GameId", "dbo.Games", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "GameId", "dbo.Games");
            DropForeignKey("dbo.Influences", "EffectId", "dbo.Effects");
            DropForeignKey("dbo.Effects", "AnswerId", "dbo.Answers");
            DropIndex("dbo.Questions", new[] { "GameId" });
            DropIndex("dbo.Influences", new[] { "EffectId" });
            DropIndex("dbo.Effects", new[] { "AnswerId" });
            AlterColumn("dbo.Questions", "GameId", c => c.Int());
            AlterColumn("dbo.Influences", "EffectId", c => c.Int());
            AlterColumn("dbo.Effects", "AnswerId", c => c.Int());
            DropColumn("dbo.Users", "QuestionId");
            RenameColumn(table: "dbo.Questions", name: "GameId", newName: "Game_Id");
            RenameColumn(table: "dbo.Influences", name: "EffectId", newName: "Effect_Id");
            RenameColumn(table: "dbo.Effects", name: "AnswerId", newName: "Answer_Id");
            CreateIndex("dbo.Questions", "Game_Id");
            CreateIndex("dbo.Influences", "Effect_Id");
            CreateIndex("dbo.Effects", "Answer_Id");
            AddForeignKey("dbo.Questions", "Game_Id", "dbo.Games", "Id");
            AddForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects", "Id");
            AddForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers", "Id");
        }
    }
}
