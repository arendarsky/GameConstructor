namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "QuestionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "QuestionId", c => c.Int(nullable: false));
        }
    }
}
