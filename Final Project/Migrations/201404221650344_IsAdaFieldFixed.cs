namespace Final_Project.Migrations.ConfigurationContext1
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsAdaFieldFixed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsAda", c => c.Boolean());
            DropColumn("dbo.AspNetUsers", "Ada");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Ada", c => c.Boolean());
            DropColumn("dbo.AspNetUsers", "IsAda");
        }
    }
}
