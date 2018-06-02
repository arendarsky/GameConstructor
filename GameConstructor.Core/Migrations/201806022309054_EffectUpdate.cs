namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EffectUpdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Effects", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Effects", "Value", c => c.Int(nullable: false));
        }
    }
}
