namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatefect : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Effects", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Effects", new[] { "AnswerId" });
            RenameColumn(table: "dbo.Effects", name: "AnswerId", newName: "Answer_Id");
            RenameColumn(table: "dbo.Answers", name: "Question_Id", newName: "QuestionId");
            AlterColumn("dbo.Answers", "QuestionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Effects", "Answer_Id", c => c.Int());
            CreateIndex("dbo.Answers", "QuestionId");
            CreateIndex("dbo.Effects", "Answer_Id");
            AddForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers", "Id");
            AddForeignKey("dbo.Answers", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers");
            DropIndex("dbo.Effects", new[] { "Answer_Id" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            AlterColumn("dbo.Effects", "Answer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "QuestionId", c => c.Int());
            RenameColumn(table: "dbo.Answers", name: "QuestionId", newName: "Question_Id");
            RenameColumn(table: "dbo.Effects", name: "Answer_Id", newName: "AnswerId");
            CreateIndex("dbo.Effects", "AnswerId");
            CreateIndex("dbo.Answers", "Question_Id");
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id");
            AddForeignKey("dbo.Effects", "AnswerId", "dbo.Answers", "Id", cascadeDelete: true);
        }
    }
}
