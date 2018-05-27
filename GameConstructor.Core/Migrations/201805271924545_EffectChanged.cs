namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EffectChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Effects", "Value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Effects", "Value", c => c.Double(nullable: false));
        }
    }
}
