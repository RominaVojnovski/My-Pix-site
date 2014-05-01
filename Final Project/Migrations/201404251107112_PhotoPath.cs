namespace Final_Project.Migrations.ConfigurationContext5
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "PhotoPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "PhotoPath");
        }
    }
}
