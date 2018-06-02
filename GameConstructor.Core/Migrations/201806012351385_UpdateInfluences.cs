namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInfluences : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects");
            DropForeignKey("dbo.Influences", "CharacteristicId", "dbo.Characteristics");
            DropIndex("dbo.Influences", new[] { "CharacteristicId" });
            DropIndex("dbo.Influences", new[] { "Effect_Id" });
            RenameColumn(table: "dbo.Influences", name: "Effect_Id", newName: "EffectId");
            RenameColumn(table: "dbo.Influences", name: "CharacteristicId", newName: "Characteristic_Id");
            AlterColumn("dbo.Influences", "Characteristic_Id", c => c.Int());
            AlterColumn("dbo.Influences", "EffectId", c => c.Int(nullable: false));
            CreateIndex("dbo.Influences", "EffectId");
            CreateIndex("dbo.Influences", "Characteristic_Id");
            AddForeignKey("dbo.Influences", "EffectId", "dbo.Effects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Influences", "Characteristic_Id", "dbo.Characteristics", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Influences", "Characteristic_Id", "dbo.Characteristics");
            DropForeignKey("dbo.Influences", "EffectId", "dbo.Effects");
            DropIndex("dbo.Influences", new[] { "Characteristic_Id" });
            DropIndex("dbo.Influences", new[] { "EffectId" });
            AlterColumn("dbo.Influences", "EffectId", c => c.Int());
            AlterColumn("dbo.Influences", "Characteristic_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Influences", name: "Characteristic_Id", newName: "CharacteristicId");
            RenameColumn(table: "dbo.Influences", name: "EffectId", newName: "Effect_Id");
            CreateIndex("dbo.Influences", "Effect_Id");
            CreateIndex("dbo.Influences", "CharacteristicId");
            AddForeignKey("dbo.Influences", "CharacteristicId", "dbo.Characteristics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Influences", "Effect_Id", "dbo.Effects", "Id");
        }
    }
}
