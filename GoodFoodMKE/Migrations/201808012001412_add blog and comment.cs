namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addblogandcomment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Farms", "LogoFilePath", c => c.String());
            AddColumn("dbo.Markets", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Markets", "LogoFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Markets", "LogoFilePath");
            DropColumn("dbo.Markets", "Active");
            DropColumn("dbo.Farms", "LogoFilePath");
        }
    }
}
