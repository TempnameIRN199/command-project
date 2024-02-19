namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd19022024 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Name", c => c.String());
            AddColumn("dbo.Users", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "BirthDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "BirthDate");
            DropColumn("dbo.Users", "CreationDate");
            DropColumn("dbo.Requests", "Name");
        }
    }
}
