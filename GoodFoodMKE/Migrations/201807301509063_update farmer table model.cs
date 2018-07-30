namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefarmertablemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "AppUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AppUsers", "Farm_Id", c => c.Int());
            CreateIndex("dbo.AppUsers", "AppUser_Id");
            CreateIndex("dbo.AppUsers", "Farm_Id");
            AddForeignKey("dbo.AppUsers", "AppUser_Id", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.AppUsers", "Farm_Id", "dbo.Farms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUsers", "Farm_Id", "dbo.Farms");
            DropForeignKey("dbo.AppUsers", "AppUser_Id", "dbo.AppUsers");
            DropIndex("dbo.AppUsers", new[] { "Farm_Id" });
            DropIndex("dbo.AppUsers", new[] { "AppUser_Id" });
            DropColumn("dbo.AppUsers", "Farm_Id");
            DropColumn("dbo.AppUsers", "AppUser_Id");
        }
    }
}
