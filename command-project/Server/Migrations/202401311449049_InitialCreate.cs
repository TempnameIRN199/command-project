namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CVs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Skills = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        UserInfo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RequestCVs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        CVId = c.Int(nullable: false),
                        RequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.CVId, cascadeDelete: true)
                .ForeignKey("dbo.Requests", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.CVId)
                .Index(t => t.RequestId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        CreationDate = c.DateTime(nullable: false),
                        RequestInfo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SecondName = c.String(),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Age = c.Int(nullable: false),
                        Status = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "UserId", "dbo.Users");
            DropForeignKey("dbo.CVs", "UserId", "dbo.Users");
            DropForeignKey("dbo.RequestCVs", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.RequestCVs", "CVId", "dbo.CVs");
            DropIndex("dbo.Requests", new[] { "UserId" });
            DropIndex("dbo.RequestCVs", new[] { "RequestId" });
            DropIndex("dbo.RequestCVs", new[] { "CVId" });
            DropIndex("dbo.CVs", new[] { "UserId" });
            DropTable("dbo.Skills");
            DropTable("dbo.Users");
            DropTable("dbo.Requests");
            DropTable("dbo.RequestCVs");
            DropTable("dbo.CVs");
        }
    }
}
