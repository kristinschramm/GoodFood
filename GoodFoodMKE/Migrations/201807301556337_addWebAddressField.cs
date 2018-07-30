namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWebAddressField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Farms", "WebAddress", c => c.String());
            AddColumn("dbo.Markets", "WebAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Markets", "WebAddress");
            DropColumn("dbo.Farms", "WebAddress");
        }
    }
}
