namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.Influences", "EffectId", "dbo.Effects");
            DropIndex("dbo.Effects", new[] { "Answer_Id" });
            DropIndex("dbo.Influences", new[] { "EffectId" });
            RenameColumn(table: "dbo.Effects", name: "Answer_Id", newName: "AnswerId");
            RenameColumn(table: "dbo.Influences", name: "EffectId", newName: "Effect_Id");
            AlterColumn("dbo.Effects", "AnswerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Influences", "Effect_Id", c => c.Int());
            CreateIndex("dbo.Effects", "AnswerId");
            CreateIndex("dbo.Influences", "Effect_Id");
            AddForeignKey("dbo.Effects", "AnswerId", "dbo.Answers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects");
            DropForeignKey("dbo.Effects", "AnswerId", "dbo.Answers");
            DropIndex("dbo.Influences", new[] { "Effect_Id" });
            DropIndex("dbo.Effects", new[] { "AnswerId" });
            AlterColumn("dbo.Influences", "Effect_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Effects", "AnswerId", c => c.Int());
            RenameColumn(table: "dbo.Influences", name: "Effect_Id", newName: "EffectId");
            RenameColumn(table: "dbo.Effects", name: "AnswerId", newName: "Answer_Id");
            CreateIndex("dbo.Influences", "EffectId");
            CreateIndex("dbo.Effects", "Answer_Id");
            AddForeignKey("dbo.Influences", "EffectId", "dbo.Effects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Effects", "Answer_Id", "dbo.Answers", "Id");
        }
    }
}
