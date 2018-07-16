namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdatabase : DbMigration
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
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LocationId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        LocationType_Id = c.Int(),
                        Market_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.LocationTypes", t => t.LocationType_Id)
                .ForeignKey("dbo.Locations", t => t.Market_Id)
                .Index(t => t.AddressId)
                .Index(t => t.LocationType_Id)
                .Index(t => t.Market_Id);
            
            CreateTable(
                "dbo.LocationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .ForeignKey("dbo.Locations", t => t.Farm_Id)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "Market_Id", "dbo.Locations");
            DropForeignKey("dbo.Locations", "LocationType_Id", "dbo.LocationTypes");
            DropForeignKey("dbo.Locations", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Products", "Farm_Id", "dbo.Locations");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropIndex("dbo.Products", new[] { "Farm_Id" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropIndex("dbo.Locations", new[] { "Market_Id" });
            DropIndex("dbo.Locations", new[] { "LocationType_Id" });
            DropIndex("dbo.Locations", new[] { "AddressId" });
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Products");
            DropTable("dbo.LocationTypes");
            DropTable("dbo.Locations");
            DropTable("dbo.Addresses");
        }
    }
}
