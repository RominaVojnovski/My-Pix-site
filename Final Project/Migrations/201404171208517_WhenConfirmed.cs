namespace Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WhenConfirmed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "WhenRegistered", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "WhenConfirmed", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "usersName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "usersName");
            DropColumn("dbo.AspNetUsers", "WhenConfirmed");
            DropColumn("dbo.AspNetUsers", "WhenRegistered");
        }
    }
}
