namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingModels3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Effects", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Influences", "EffectId", "dbo.Effects");
            DropForeignKey("dbo.Characteristics", "GameId", "dbo.Games");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Effects", new[] { "AnswerId" });
            DropIndex("dbo.Influences", new[] { "EffectId" });
            DropIndex("dbo.Characteristics", new[] { "GameId" });
            RenameColumn(table: "dbo.Effects", name: "AnswerId", newName: "Answer_Id");
            RenameColumn(table: "dbo.Influences", name: "EffectId", newName: "Effect_Id");
            RenameColumn(table: "dbo.Characteristics", name: "GameId", newName: "Game_Id");
            RenameColumn(table: "dbo.Answers", name: "QuestionId", newName: "Question_Id");
            AlterColumn("dbo.Answers", "Question_Id", c => c.Int());
            AlterColumn("dbo.Effects", "Answer_Id", c => c.Int());
            AlterColumn("dbo.Influences", "Effect_Id", c => c.Int());
            AlterColumn("dbo.Characteristics", "Game_Id", c => c.Int());
            CreateIndex("dbo.Answers", "Question_Id");
            CreateIndex("dbo.Effects", "Answer_Id");
            CreateIndex("dbo.Influences", "Effect_Id");
            CreateIndex("dbo.Characteristics", "Game_Id");
            AddForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers", "Id");
            AddForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects", "Id");
            AddForeignKey("dbo.Characteristics", "Game_Id", "dbo.Games", "Id");
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Characteristics", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects");
            DropForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers");
            DropIndex("dbo.Characteristics", new[] { "Game_Id" });
            DropIndex("dbo.Influences", new[] { "Effect_Id" });
            DropIndex("dbo.Effects", new[] { "Answer_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            AlterColumn("dbo.Characteristics", "Game_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Influences", "Effect_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Effects", "Answer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "Question_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Answers", name: "Question_Id", newName: "QuestionId");
            RenameColumn(table: "dbo.Characteristics", name: "Game_Id", newName: "GameId");
            RenameColumn(table: "dbo.Influences", name: "Effect_Id", newName: "EffectId");
            RenameColumn(table: "dbo.Effects", name: "Answer_Id", newName: "AnswerId");
            CreateIndex("dbo.Characteristics", "GameId");
            CreateIndex("dbo.Influences", "EffectId");
            CreateIndex("dbo.Effects", "AnswerId");
            CreateIndex("dbo.Answers", "QuestionId");
            AddForeignKey("dbo.Answers", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Characteristics", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Influences", "EffectId", "dbo.Effects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Effects", "AnswerId", "dbo.Answers", "Id", cascadeDelete: true);
        }
    }
}
