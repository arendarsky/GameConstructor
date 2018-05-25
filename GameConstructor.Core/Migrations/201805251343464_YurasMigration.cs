namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YurasMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characteristics", "Value", c => c.Int(nullable: false));
            DropColumn("dbo.Characteristics", "Level");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Characteristics", "Level", c => c.Double(nullable: false));
            DropColumn("dbo.Characteristics", "Value");
        }
    }
}
