namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFarmActiveBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Farms", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Farms", "Active");
        }
    }
}
