namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressString = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NameFirst = c.String(nullable: false),
                        NameLast = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                        GravatarEmailHash = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                        Farm_Id = c.Int(),
                        Market_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Farms", t => t.Farm_Id)
                .ForeignKey("dbo.Markets", t => t.Market_Id)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Farm_Id)
                .Index(t => t.Market_Id);
            
            CreateTable(
                "dbo.BlogEntries",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        ImagePath = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Approved = c.Boolean(nullable: false),
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
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AddressId = c.Int(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        Recurring = c.Boolean(nullable: false),
                        RecurringType = c.String(),
                        Creator_Id = c.String(maxLength: 128),
                        Host_Id = c.Int(),
                        Market_Id = c.Int(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.AppUsers", t => t.Creator_Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Farms", t => t.Host_Id)
                .ForeignKey("dbo.Markets", t => t.Market_Id)
                .Index(t => t.AddressId)
                .Index(t => t.Creator_Id)
                .Index(t => t.Host_Id)
                .Index(t => t.Market_Id);
            
            CreateTable(
                "dbo.Farms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddressId = c.Int(nullable: false),
                        WebAddress = c.String(),
                        RequestorId = c.String(maxLength: 128),
                        Active = c.Boolean(nullable: false),
                        LogoFilePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.RequestorId)
                .Index(t => t.AddressId)
                .Index(t => t.RequestorId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductTypeId = c.Int(nullable: false),
                        Farm_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Farms", t => t.Farm_Id)
                .Index(t => t.ProductTypeId)
                .Index(t => t.Farm_Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Markets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddressId = c.Int(nullable: false),
                        WebAddress = c.String(),
                        RequestorId = c.String(maxLength: 128),
                        Active = c.Boolean(nullable: false),
                        LogoFilePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.RequestorId)
                .Index(t => t.AddressId)
                .Index(t => t.RequestorId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Events", "Market_Id", "dbo.Markets");
            DropForeignKey("dbo.Markets", "RequestorId", "dbo.AppUsers");
            DropForeignKey("dbo.Markets", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.AppUsers", "Market_Id", "dbo.Markets");
            DropForeignKey("dbo.Events", "Host_Id", "dbo.Farms");
            DropForeignKey("dbo.Farms", "RequestorId", "dbo.AppUsers");
            DropForeignKey("dbo.Products", "Farm_Id", "dbo.Farms");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.Farms", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.AppUsers", "Farm_Id", "dbo.Farms");
            DropForeignKey("dbo.Events", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Events", "Creator_Id", "dbo.AppUsers");
            DropForeignKey("dbo.BlogEntries", "Creator_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "BlogId", "dbo.BlogEntries");
            DropForeignKey("dbo.Comments", "HeadComment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Commentor_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUsers", "AppUser_Id", "dbo.AppUsers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Markets", new[] { "RequestorId" });
            DropIndex("dbo.Markets", new[] { "AddressId" });
            DropIndex("dbo.Products", new[] { "Farm_Id" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropIndex("dbo.Farms", new[] { "RequestorId" });
            DropIndex("dbo.Farms", new[] { "AddressId" });
            DropIndex("dbo.Events", new[] { "Market_Id" });
            DropIndex("dbo.Events", new[] { "Host_Id" });
            DropIndex("dbo.Events", new[] { "Creator_Id" });
            DropIndex("dbo.Events", new[] { "AddressId" });
            DropIndex("dbo.Comments", new[] { "HeadComment_CommentId" });
            DropIndex("dbo.Comments", new[] { "Commentor_Id" });
            DropIndex("dbo.Comments", new[] { "BlogId" });
            DropIndex("dbo.BlogEntries", new[] { "Creator_Id" });
            DropIndex("dbo.AppUsers", new[] { "Market_Id" });
            DropIndex("dbo.AppUsers", new[] { "Farm_Id" });
            DropIndex("dbo.AppUsers", new[] { "AppUser_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Markets");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Products");
            DropTable("dbo.Farms");
            DropTable("dbo.Events");
            DropTable("dbo.Comments");
            DropTable("dbo.BlogEntries");
            DropTable("dbo.AppUsers");
            DropTable("dbo.Addresses");
        }
    }
}
