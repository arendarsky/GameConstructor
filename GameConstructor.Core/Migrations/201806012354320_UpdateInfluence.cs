namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInfluence : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Influences", "Characteristic_Id", "dbo.Characteristics");
            DropIndex("dbo.Influences", new[] { "Characteristic_Id" });
            RenameColumn(table: "dbo.Influences", name: "Characteristic_Id", newName: "CharacteristicId");
            AlterColumn("dbo.Influences", "CharacteristicId", c => c.Int());
            CreateIndex("dbo.Influences", "CharacteristicId");
            AddForeignKey("dbo.Influences", "CharacteristicId", "dbo.Characteristics", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Influences", "CharacteristicId", "dbo.Characteristics");
            DropIndex("dbo.Influences", new[] { "CharacteristicId" });
            AlterColumn("dbo.Influences", "CharacteristicId", c => c.Int());
            RenameColumn(table: "dbo.Influences", name: "CharacteristicId", newName: "Characteristic_Id");
            CreateIndex("dbo.Influences", "Characteristic_Id");
            AddForeignKey("dbo.Influences", "Characteristic_Id", "dbo.Characteristics", "Id");
        }
    }
}
