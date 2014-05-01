namespace Final_Project.Migrations.ConfigurationContext1
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userprofiletext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserProfileText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserProfileText");
        }
    }
}
