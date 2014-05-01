namespace Final_Project.Migrations.ConfigurationContext5
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Poster_addPosterName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posters", "PosterName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posters", "PosterName");
        }
    }
}
