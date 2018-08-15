namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductsToFarm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Farms", "Products", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Farms", "Products");
        }
    }
}
