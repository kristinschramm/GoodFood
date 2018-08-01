namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcommentsandblogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogEntries",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        ImagePath = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Creator_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.AppUsers", t => t.Creator_Id)
                .Index(t => t.Creator_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentString = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpVote = c.Int(nullable: false),
                        DownVote = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                        Commentor_Id = c.String(maxLength: 128),
                        HeadComment_CommentId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.AppUsers", t => t.Commentor_Id)
                .ForeignKey("dbo.Comments", t => t.HeadComment_CommentId)
                .ForeignKey("dbo.BlogEntries", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId)
                .Index(t => t.Commentor_Id)
                .Index(t => t.HeadComment_CommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogEntries", "Creator_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "BlogId", "dbo.BlogEntries");
            DropForeignKey("dbo.Comments", "HeadComment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Commentor_Id", "dbo.AppUsers");
            DropIndex("dbo.Comments", new[] { "HeadComment_CommentId" });
            DropIndex("dbo.Comments", new[] { "Commentor_Id" });
            DropIndex("dbo.Comments", new[] { "BlogId" });
            DropIndex("dbo.BlogEntries", new[] { "Creator_Id" });
            DropTable("dbo.Comments");
            DropTable("dbo.BlogEntries");
        }
    }
}
