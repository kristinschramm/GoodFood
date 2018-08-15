namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "DayOfWeek", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Time");
            DropColumn("dbo.Events", "DayOfWeek");
        }
    }
}
