namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects");
            DropForeignKey("dbo.Questions", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Effects", new[] { "Answer_Id" });
            DropIndex("dbo.Influences", new[] { "Effect_Id" });
            DropIndex("dbo.Questions", new[] { "Game_Id" });
            AlterColumn("dbo.Answers", "Question_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Effects", "Answer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Influences", "Effect_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "Game_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Answers", "Question_Id");
            CreateIndex("dbo.Effects", "Answer_Id");
            CreateIndex("dbo.Influences", "Effect_Id");
            CreateIndex("dbo.Questions", "Game_Id");
            AddForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Questions", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects");
            DropForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers");
            DropIndex("dbo.Questions", new[] { "Game_Id" });
            DropIndex("dbo.Influences", new[] { "Effect_Id" });
            DropIndex("dbo.Effects", new[] { "Answer_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            AlterColumn("dbo.Questions", "Game_Id", c => c.Int());
            AlterColumn("dbo.Influences", "Effect_Id", c => c.Int());
            AlterColumn("dbo.Effects", "Answer_Id", c => c.Int());
            AlterColumn("dbo.Answers", "Question_Id", c => c.Int());
            CreateIndex("dbo.Questions", "Game_Id");
            CreateIndex("dbo.Influences", "Effect_Id");
            CreateIndex("dbo.Effects", "Answer_Id");
            CreateIndex("dbo.Answers", "Question_Id");
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id");
            AddForeignKey("dbo.Questions", "Game_Id", "dbo.Games", "Id");
            AddForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects", "Id");
            AddForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers", "Id");
        }
    }
}
