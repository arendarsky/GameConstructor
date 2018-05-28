namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeModeslsChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "Picture_Id", "dbo.Pictures");
            DropForeignKey("dbo.Games", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects");
            DropForeignKey("dbo.Influences", "Characteristic_Id", "dbo.Characteristics");
            DropIndex("dbo.Games", new[] { "Picture_Id" });
            DropIndex("dbo.Games", new[] { "User_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Effects", new[] { "Answer_Id" });
            DropIndex("dbo.Influences", new[] { "Characteristic_Id" });
            DropIndex("dbo.Influences", new[] { "Effect_Id" });
            RenameColumn(table: "dbo.Games", name: "Picture_Id", newName: "PictureId");
            RenameColumn(table: "dbo.Games", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Answers", name: "Question_Id", newName: "QuestionId");
            RenameColumn(table: "dbo.Effects", name: "Answer_Id", newName: "AnswerId");
            RenameColumn(table: "dbo.Influences", name: "Effect_Id", newName: "EffectId");
            RenameColumn(table: "dbo.Influences", name: "Characteristic_Id", newName: "CharacteristicId");
            AlterColumn("dbo.Games", "PictureId", c => c.Int(nullable: false));
            AlterColumn("dbo.Games", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "QuestionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Effects", "AnswerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Influences", "CharacteristicId", c => c.Int(nullable: false));
            AlterColumn("dbo.Influences", "EffectId", c => c.Int(nullable: false));
            CreateIndex("dbo.Answers", "QuestionId");
            CreateIndex("dbo.Effects", "AnswerId");
            CreateIndex("dbo.Influences", "CharacteristicId");
            CreateIndex("dbo.Influences", "EffectId");
            CreateIndex("dbo.Games", "PictureId");
            CreateIndex("dbo.Games", "UserId");
            AddForeignKey("dbo.Games", "PictureId", "dbo.Pictures", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Games", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Effects", "AnswerId", "dbo.Answers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Influences", "EffectId", "dbo.Effects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Influences", "CharacteristicId", "dbo.Characteristics", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Influences", "CharacteristicId", "dbo.Characteristics");
            DropForeignKey("dbo.Influences", "EffectId", "dbo.Effects");
            DropForeignKey("dbo.Effects", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Games", "UserId", "dbo.Users");
            DropForeignKey("dbo.Games", "PictureId", "dbo.Pictures");
            DropIndex("dbo.Games", new[] { "UserId" });
            DropIndex("dbo.Games", new[] { "PictureId" });
            DropIndex("dbo.Influences", new[] { "EffectId" });
            DropIndex("dbo.Influences", new[] { "CharacteristicId" });
            DropIndex("dbo.Effects", new[] { "AnswerId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            AlterColumn("dbo.Influences", "EffectId", c => c.Int());
            AlterColumn("dbo.Influences", "CharacteristicId", c => c.Int());
            AlterColumn("dbo.Effects", "AnswerId", c => c.Int());
            AlterColumn("dbo.Answers", "QuestionId", c => c.Int());
            AlterColumn("dbo.Games", "UserId", c => c.Int());
            AlterColumn("dbo.Games", "PictureId", c => c.Int());
            RenameColumn(table: "dbo.Influences", name: "CharacteristicId", newName: "Characteristic_Id");
            RenameColumn(table: "dbo.Influences", name: "EffectId", newName: "Effect_Id");
            RenameColumn(table: "dbo.Effects", name: "AnswerId", newName: "Answer_Id");
            RenameColumn(table: "dbo.Answers", name: "QuestionId", newName: "Question_Id");
            RenameColumn(table: "dbo.Games", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Games", name: "PictureId", newName: "Picture_Id");
            CreateIndex("dbo.Influences", "Effect_Id");
            CreateIndex("dbo.Influences", "Characteristic_Id");
            CreateIndex("dbo.Effects", "Answer_Id");
            CreateIndex("dbo.Answers", "Question_Id");
            CreateIndex("dbo.Games", "User_Id");
            CreateIndex("dbo.Games", "Picture_Id");
            AddForeignKey("dbo.Influences", "Characteristic_Id", "dbo.Characteristics", "Id");
            AddForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects", "Id");
            AddForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers", "Id");
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id");
            AddForeignKey("dbo.Games", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Games", "Picture_Id", "dbo.Pictures", "Id");
        }
    }
}
