namespace Final_Project.Migrations.ConfigurationContext4
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedResponseTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        WhenPosted = c.DateTime(nullable: false),
                        Title = c.String(),
                        CommentText = c.String(),
                        Photo_PhotoId = c.Int(),
                        Poster_PosterId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Photos", t => t.Photo_PhotoId)
                .ForeignKey("dbo.Posters", t => t.Poster_PosterId)
                .Index(t => t.Photo_PhotoId)
                .Index(t => t.Poster_PosterId);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ResponseId = c.Int(nullable: false, identity: true),
                        WhenPosted = c.DateTime(nullable: false),
                        ResponseText = c.String(),
                        Comment_CommentId = c.Int(),
                        Poster_PosterId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ResponseId)
                .ForeignKey("dbo.Comments", t => t.Comment_CommentId)
                .ForeignKey("dbo.Posters", t => t.Poster_PosterId)
                .Index(t => t.Comment_CommentId)
                .Index(t => t.Poster_PosterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "Poster_PosterId", "dbo.Posters");
            DropForeignKey("dbo.Responses", "Comment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Poster_PosterId", "dbo.Posters");
            DropForeignKey("dbo.Comments", "Photo_PhotoId", "dbo.Photos");
            DropIndex("dbo.Responses", new[] { "Poster_PosterId" });
            DropIndex("dbo.Responses", new[] { "Comment_CommentId" });
            DropIndex("dbo.Comments", new[] { "Poster_PosterId" });
            DropIndex("dbo.Comments", new[] { "Photo_PhotoId" });
            DropTable("dbo.Responses");
            DropTable("dbo.Comments");
        }
    }
}
