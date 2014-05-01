namespace Final_Project.Migrations.ConfigurationContext4
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoTagsRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagsPhotoes",
                c => new
                    {
                        Tags_TagsId = c.Int(nullable: false),
                        Photo_PhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tags_TagsId, t.Photo_PhotoId })
                .ForeignKey("dbo.Tags", t => t.Tags_TagsId, cascadeDelete: true)
                .ForeignKey("dbo.Photos", t => t.Photo_PhotoId, cascadeDelete: true)
                .Index(t => t.Tags_TagsId)
                .Index(t => t.Photo_PhotoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagsPhotoes", "Photo_PhotoId", "dbo.Photos");
            DropForeignKey("dbo.TagsPhotoes", "Tags_TagsId", "dbo.Tags");
            DropIndex("dbo.TagsPhotoes", new[] { "Photo_PhotoId" });
            DropIndex("dbo.TagsPhotoes", new[] { "Tags_TagsId" });
            DropTable("dbo.TagsPhotoes");
        }
    }
}
