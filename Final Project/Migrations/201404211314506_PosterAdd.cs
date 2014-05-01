namespace Final_Project.Migrations.ConfigurationContext4
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PosterAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posters",
                c => new
                    {
                        PosterId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PosterId);
            
            AddColumn("dbo.Photos", "Poster_PosterId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Photos", "Poster_PosterId");
            AddForeignKey("dbo.Photos", "Poster_PosterId", "dbo.Posters", "PosterId");
            DropColumn("dbo.Photos", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photos", "UserId", c => c.String());
            DropForeignKey("dbo.Photos", "Poster_PosterId", "dbo.Posters");
            DropIndex("dbo.Photos", new[] { "Poster_PosterId" });
            DropColumn("dbo.Photos", "Poster_PosterId");
            DropTable("dbo.Posters");
        }
    }
}
