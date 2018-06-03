namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reincarnation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Effects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        AnswerId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .Index(t => t.AnswerId);
            
            CreateTable(
                "dbo.Influences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CharacteristicId = c.Int(nullable: false),
                        EffectId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characteristics", t => t.CharacteristicId, cascadeDelete: true)
                .ForeignKey("dbo.Effects", t => t.EffectId, cascadeDelete: true)
                .Index(t => t.CharacteristicId)
                .Index(t => t.EffectId);
            
            CreateTable(
                "dbo.Characteristics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GameId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Source = c.String(),
                        DisplayingInGameMode = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        Popularity = c.Int(nullable: false),
                        Picture_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.Picture_Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Picture_Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageSource = c.String(),
                        IsBorderRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "UserId", "dbo.Users");
            DropForeignKey("dbo.Results", "GameId", "dbo.Games");
            DropForeignKey("dbo.Questions", "GameId", "dbo.Games");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Games", "Picture_Id", "dbo.Pictures");
            DropForeignKey("dbo.Characteristics", "GameId", "dbo.Games");
            DropForeignKey("dbo.Effects", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Influences", "EffectId", "dbo.Effects");
            DropForeignKey("dbo.Influences", "CharacteristicId", "dbo.Characteristics");
            DropIndex("dbo.Results", new[] { "GameId" });
            DropIndex("dbo.Questions", new[] { "GameId" });
            DropIndex("dbo.Games", new[] { "Picture_Id" });
            DropIndex("dbo.Games", new[] { "UserId" });
            DropIndex("dbo.Characteristics", new[] { "GameId" });
            DropIndex("dbo.Influences", new[] { "EffectId" });
            DropIndex("dbo.Influences", new[] { "CharacteristicId" });
            DropIndex("dbo.Effects", new[] { "AnswerId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Users");
            DropTable("dbo.Results");
            DropTable("dbo.Questions");
            DropTable("dbo.Pictures");
            DropTable("dbo.Games");
            DropTable("dbo.Characteristics");
            DropTable("dbo.Influences");
            DropTable("dbo.Effects");
            DropTable("dbo.Answers");
        }
    }
}
