namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMarket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "Market_Id", c => c.Int());
            CreateIndex("dbo.AppUsers", "Market_Id");
            AddForeignKey("dbo.AppUsers", "Market_Id", "dbo.Markets", "Id");
            DropColumn("dbo.Farms", "LocationId");
            DropColumn("dbo.Markets", "LocationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Markets", "LocationId", c => c.Int(nullable: false));
            AddColumn("dbo.Farms", "LocationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AppUsers", "Market_Id", "dbo.Markets");
            DropIndex("dbo.AppUsers", new[] { "Market_Id" });
            DropColumn("dbo.AppUsers", "Market_Id");
        }
    }
}
