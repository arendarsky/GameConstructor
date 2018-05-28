namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BugFixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Effects", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Influences", "EffectId", "dbo.Effects");
            DropForeignKey("dbo.Influences", "CharacteristicId", "dbo.Characteristics");
            DropForeignKey("dbo.Games", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.Games", "UserId", "dbo.Users");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Effects", new[] { "AnswerId" });
            DropIndex("dbo.Influences", new[] { "CharacteristicId" });
            DropIndex("dbo.Influences", new[] { "EffectId" });
            DropIndex("dbo.Games", new[] { "PictureId" });
            DropIndex("dbo.Games", new[] { "UserId" });
            RenameColumn(table: "dbo.Effects", name: "AnswerId", newName: "Answer_Id");
            RenameColumn(table: "dbo.Influences", name: "EffectId", newName: "Effect_Id");
            RenameColumn(table: "dbo.Influences", name: "CharacteristicId", newName: "Characteristic_Id");
            RenameColumn(table: "dbo.Games", name: "PictureId", newName: "Picture_Id");
            RenameColumn(table: "dbo.Games", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Answers", name: "QuestionId", newName: "Question_Id");
            AlterColumn("dbo.Answers", "Question_Id", c => c.Int());
            AlterColumn("dbo.Effects", "Answer_Id", c => c.Int());
            AlterColumn("dbo.Influences", "Characteristic_Id", c => c.Int());
            AlterColumn("dbo.Influences", "Effect_Id", c => c.Int());
            AlterColumn("dbo.Games", "Picture_Id", c => c.Int());
            AlterColumn("dbo.Games", "User_Id", c => c.Int());
            CreateIndex("dbo.Games", "Picture_Id");
            CreateIndex("dbo.Games", "User_Id");
            CreateIndex("dbo.Answers", "Question_Id");
            CreateIndex("dbo.Effects", "Answer_Id");
            CreateIndex("dbo.Influences", "Characteristic_Id");
            CreateIndex("dbo.Influences", "Effect_Id");
            AddForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers", "Id");
            AddForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects", "Id");
            AddForeignKey("dbo.Influences", "Characteristic_Id", "dbo.Characteristics", "Id");
            AddForeignKey("dbo.Games", "Picture_Id", "dbo.Pictures", "Id");
            AddForeignKey("dbo.Games", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Games", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Games", "Picture_Id", "dbo.Pictures");
            DropForeignKey("dbo.Influences", "Characteristic_Id", "dbo.Characteristics");
            DropForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects");
            DropForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers");
            DropIndex("dbo.Influences", new[] { "Effect_Id" });
            DropIndex("dbo.Influences", new[] { "Characteristic_Id" });
            DropIndex("dbo.Effects", new[] { "Answer_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Games", new[] { "User_Id" });
            DropIndex("dbo.Games", new[] { "Picture_Id" });
            AlterColumn("dbo.Games", "User_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Games", "Picture_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Influences", "Effect_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Influences", "Characteristic_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Effects", "Answer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "Question_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Answers", name: "Question_Id", newName: "QuestionId");
            RenameColumn(table: "dbo.Games", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Games", name: "Picture_Id", newName: "PictureId");
            RenameColumn(table: "dbo.Influences", name: "Characteristic_Id", newName: "CharacteristicId");
            RenameColumn(table: "dbo.Influences", name: "Effect_Id", newName: "EffectId");
            RenameColumn(table: "dbo.Effects", name: "Answer_Id", newName: "AnswerId");
            CreateIndex("dbo.Games", "UserId");
            CreateIndex("dbo.Games", "PictureId");
            CreateIndex("dbo.Influences", "EffectId");
            CreateIndex("dbo.Influences", "CharacteristicId");
            CreateIndex("dbo.Effects", "AnswerId");
            CreateIndex("dbo.Answers", "QuestionId");
            AddForeignKey("dbo.Answers", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Games", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Games", "PictureId", "dbo.Pictures", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Influences", "CharacteristicId", "dbo.Characteristics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Influences", "EffectId", "dbo.Effects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Effects", "AnswerId", "dbo.Answers", "Id", cascadeDelete: true);
        }
    }
}
